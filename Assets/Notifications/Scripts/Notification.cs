using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public TextMeshProUGUI notificationText, objRequiredText;
    public Image timerImage, resultImage;
    public Sprite successSprite, failSprite;
    public int position;
    public float fillAmount;
    private float _duration;
    public bool shown;
    private bool _eventCompleted, _notificationEnded;
    private string _debugMessage;
    private float _timer;

    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (shown && !_notificationEnded)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                fillAmount = _timer / _duration;
                if (timerImage) timerImage.fillAmount = fillAmount;
            }
            else if (_debugMessage == null && !_eventCompleted)
            {
                EndNotification(false);
            }
            else Hide();
        }
    }

    //Standard notification
    public Notification Show(string text, float duration, int pos, string objRequired)
    {
        gameObject.SetActive(true);
        
        objRequiredText.text = "";
        if (objRequired.Length > 0)
        {
            Collectable collectable = Inventory.Instance.FindCollectableInScene(objRequired);
            if (collectable) objRequiredText.text = "Requires " + collectable.data.itemName;
            else if (Guid.TryParse(objRequired, out Guid parsedGuid)) objRequiredText.text = "Requires Package";
        }
        
        position = pos;
        transform.position = NotificationManager.Instance.hiddenPosition.position + (Vector3.down * (80 * position));
        notificationText.text = text;
        _duration = duration;
        _timer = duration;
        transform.DOMoveX(NotificationManager.Instance.hiddenPosition.position.x - 350, 0.5f);
        shown = true;
        return this;
    }

    //Debug notification
    public Notification Show(LogType logType, string condition, float duration, int pos)
    {
        gameObject.SetActive(true);
        position = pos;
        transform.position = NotificationManager.Instance.hiddenPosition.position + (Vector3.down * (80 * position));
        
        _debugMessage = condition;
        _duration = duration;
        _timer = duration;
        transform.DOMoveX(NotificationManager.Instance.hiddenPosition.position.x - 350, 0.5f);
        shown = true;
        return this;
    }

    public void Accept()
    {
        if (!String.IsNullOrEmpty(_debugMessage))
        {
            string cachedMessage = _debugMessage;
            Disable();
            NotificationManager.Instance.ShowNotification(cachedMessage, 5f, "", position);
        }
        else
        {
            Hide();
        }
    }

    public void EndNotification(bool success)
    {
        resultImage.gameObject.SetActive(true);
        resultImage.sprite = success ? successSprite : failSprite;
        resultImage.color = success ? Color.green : Color.red;
        _notificationEnded = true;
        _eventCompleted = success;

        if(_anim) _anim.SetTrigger("Ping");
        StartCoroutine(nameof(DelayedHide));
    }

    private IEnumerator DelayedHide()
    {
        yield return new WaitForSeconds(2f);
        Hide();
    }

    public void Hide(bool disable = true)
    {
        shown = false;
        transform.DOMoveX(NotificationManager.Instance.hiddenPosition.position.x + 375, 1f);
        if (disable) Invoke(nameof(Disable), 1);
    }

    public void Disable()
    {
        _debugMessage = null;
        _notificationEnded = false;
        _eventCompleted = false;

        NotificationManager.Instance.ClearPosition(position, this);
        try
        {
            resultImage.gameObject.SetActive(false);
        }
        catch
        {
            Debug.Log("oye");
        }
        gameObject.SetActive(false);
    }
}
