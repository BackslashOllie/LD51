using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogManager : MonoBehaviour
{
    public TMP_Text HighScoreText , livesRemaining;
    static LogManager _instance;
    public float timeSurvived;
    public float score;
    public int tasksCompletedInTime;
    public int tasksCompletedLate;
    public int tasksFailed;


    public static LogData log;

    public float winLimit = 300;
    public int loseLimit = 5;

    private void Awake()
    {
        _instance = this;
        score = 0;
        timeSurvived = 0;
        tasksCompletedInTime = 0;
        tasksCompletedLate = 0;
        tasksFailed = 0;

        _instance.livesRemaining.text = (_instance.loseLimit - _instance.tasksFailed).ToString() + " More Accidents Until Formal Review ";
        log = new LogData();
        log.Init();

    }

    public int GetScore
    {
        get { return Mathf.RoundToInt(score); }
    }


    public static void IncreaseTimeSurvived(float dt)
    {
        _instance.timeSurvived += dt;

        if (_instance.timeSurvived > _instance.winLimit)
        {
            NotificationManager.Instance.RemoveAllNotifications();
            
            GameManager.Instance.SetScore(_instance.score);
            GameManager.Instance.SetVictory(true);
            
            GameManager.Instance.GoToEndScene();

        }
    }

    private void FinalizeGame(bool won)
    {

    }

    public static void IncreaseCompletedTasks(float completed)
    {
        _instance.tasksCompletedInTime++;
        _instance.score += completed;

    }

    public static void IncreaseCompletedLate(int importance)
    {
        _instance.tasksCompletedLate++;
        _instance.score -= importance;
    }

    public static void IncreaseFailed()
    {
        _instance.tasksFailed++;

        _instance.livesRemaining.text = (_instance.loseLimit - _instance.tasksFailed).ToString() + " More Accidents Until Formal Review ";


        if(_instance.tasksFailed > _instance.loseLimit)
        {
            NotificationManager.Instance.RemoveAllNotifications();
            
            GameManager.Instance.SetScore(_instance.score);
            GameManager.Instance.SetVictory(false);
            GameManager.Instance.GoToEndScene();
        
        }
    }

    public static void UpdateHighScoreText()
    {
        _instance.HighScoreText.text = _instance.GetScore.ToString();
    }
}


public class LogData
{
    public int coffeesDelivered,
                    filtersChanged,
                    toiletsFixed,
                    firesExtinguished,
                    boxesDelivered,
                    serversReset,
                    printersUnjammed,
                    thirstyPeople,
                    filtersUnchanged,
                    toiletsStillBroken,
                    firesStillBurning,
                    boxesUndelivered,
                    serversDown,
                    printersJammed,
                    lightBulbsChanged,
                    BSODsFixed,
                    lightsStillFlickering,
                    BSODsUnresolved;

    public void Init()
    {
        coffeesDelivered = 0;
        filtersChanged = 0;
        toiletsFixed = 0;
        firesExtinguished = 0;
        boxesDelivered = 0;
        serversReset = 0;
        printersUnjammed = 0;
        thirstyPeople = 0;
        filtersUnchanged = 0;
        toiletsStillBroken = 0;
        firesStillBurning = 0;
        boxesUndelivered = 0;
        serversDown = 0;
        printersJammed = 0;
        lightBulbsChanged = 0;
        BSODsFixed = 0;
        lightsStillFlickering = 0;
        BSODsUnresolved = 0;
    }



}
