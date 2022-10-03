using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private float gameScore = 0;
    private bool wonGame = false;
    private bool newHighScore = false;


    private string highScoreKey = "HighScore";

    // Start is called before the first frame update
    
    public void SetScore(float sessionScore)
    {
        gameScore = sessionScore;
        var currentHighScore = PlayerPrefs.GetFloat(highScoreKey, 0);
        if (sessionScore>currentHighScore)
        {
            PlayerPrefs.SetFloat(highScoreKey, sessionScore);
            newHighScore = true;
        }
    }

    public void ResetCurrentSession()
    {
        wonGame = false;
        gameScore = 0;
        newHighScore = false;
    }


    public void SetVictory(bool didWin)
    {
        wonGame = didWin;
    }

    public bool GetNewHighScore()
    {
        return newHighScore;
    }

    public void GoToEndScene()
    {
        SceneManager.LoadScene("EndGame");
    }

    public float GetScore()
    {
        return gameScore;
    }

    public bool GetVictory()
    {
        return wonGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
