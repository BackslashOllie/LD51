using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NotificationManager : Singleton<NotificationManager>
{
    public Transform notificationsHolder, hiddenPosition;
    public Notification standardNotificationPrefab, DebugNotificationPrefab;

    public bool notifyErrors, notifyExceptions, notifyWarnings;
    public int poolAmount = 3;
    public int maxNotifications = 10;

    private bool debugNotificationsEnabled => notifyErrors || notifyExceptions || notifyWarnings;
    private List<Notification> _standardNotifications, _inviteNotifications, _debugNotifications;
    private Notification[] _occupiedPositions;

    #region Unity Callbacks

    private void OnEnable()
    {
        _occupiedPositions = new Notification[maxNotifications];
        Application.logMessageReceived += LogCallback;
    }

    private void OnDisable()
    {
        Application.RegisterLogCallback(null);
    }

    private new void Awake()
    {
        base.Awake();

        //Pool Standard Notifications
        _standardNotifications = new List<Notification>();
        for (int i = 0; i < poolAmount; i++)
        {
            Notification notification = Instantiate(standardNotificationPrefab, hiddenPosition.position,
                Quaternion.identity, notificationsHolder);
            notification.gameObject.SetActive(false);
            _standardNotifications.Add(notification);
        }

        //Pool Debug Notifications
        if (debugNotificationsEnabled)
        {
            _debugNotifications = new List<Notification>();
            for (int i = 0; i < poolAmount; i++)
            {
                Notification notification = Instantiate(DebugNotificationPrefab, hiddenPosition.position,
                    Quaternion.identity, notificationsHolder);
                notification.gameObject.SetActive(false);
                _debugNotifications.Add(notification);
            }
        }
    }

    #endregion

    #region Public Methods

    public Notification ShowNotification(string text, float duration, string objRequired, int forcedPosition = -1)
    {
        Notification notification = null;
        int position = forcedPosition == -1 ? GetEmptyPosition() : forcedPosition;
        if (position != -1)
        {
            notification = GetNotification(NotificationType.Standard);
            _occupiedPositions[position] = notification.Show(text, duration, position, objRequired);
            
        }
        else Debug.Log("Warning: Max notification limit exceeded");
        return notification;
    }

    public void ShowNotification(LogType type, string condition, float duration, int forcedPosition = -1)
    {
        int position = forcedPosition == -1 ? GetEmptyPosition() : forcedPosition;
        if (position != -1)
            _occupiedPositions[position] =
                GetNotification(NotificationType.Debug).Show(type, condition, duration, position);
        else Debug.Log("Warning: Max notification limit exceeded");
    }

    public void ClearPosition(int position, Notification notification)
    {
        if (notification == _occupiedPositions[position])
            _occupiedPositions[position] = null;
    }

    public void RemoveAllNotifications()
    {
        foreach (var n in _occupiedPositions)
        {
           if (n) n.Disable(); 
        }
    }

    #endregion

    #region Private Methods

    private Notification GetNotification(NotificationType type)
    {
        List<Notification> notificationList = null;
        Notification notificationPrefab;
        switch (type)
        {
            case NotificationType.Standard:
                notificationList = _standardNotifications;
                notificationPrefab = standardNotificationPrefab;
                break;
            case NotificationType.Debug:
                notificationList = _debugNotifications;
                notificationPrefab = DebugNotificationPrefab;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        for (int i = 0; i < notificationList.Count; i++)
            if (notificationList[i])
                if (!notificationList[i].gameObject.activeInHierarchy)
                    return notificationList[i];
        Notification notification = Instantiate(notificationPrefab, hiddenPosition.position, Quaternion.identity,
            notificationsHolder);
        notificationList.Add(notification);
        return notification;
    }

    private int GetEmptyPosition()
    {
        for (int i = 0; i < _occupiedPositions.Length; i++)
        {
            if (!_occupiedPositions[i])
            {
                return i;
            }
        }

        return -1;
    }

    void LogCallback(string condition, string stackTrace, LogType type)
    {
        if (Application.isPlaying)
        {
            bool show = (type == LogType.Error && notifyErrors) ||
                        (type == LogType.Exception && notifyExceptions) ||
                        (type == LogType.Warning && notifyWarnings);
            if (show)
                Instance.ShowNotification(type, condition, 10f);
        }
    }

    #endregion
}
