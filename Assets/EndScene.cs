using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public TextMeshProUGUI victoryState;
    public TextMeshProUGUI sessionScore;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI finalGoodComments;
    public TextMeshProUGUI finalBadComments;

    private string highScoreRef = "HighScore";
    private int score = 0;
    private bool gameWon = false;
    private int currentHighScore = 0;
    private bool newHighScore = false;
    private int coffees = 0;

    private void Start()
    {
        currentHighScore = (int) PlayerPrefs.GetFloat(highScoreRef);
        gameWon = GameManager.Instance.GetVictory();
        score = (int) GameManager.Instance.GetScore();
        newHighScore = GameManager.Instance.GetNewHighScore();

        PopulateScoreInfo();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void PopulateScoreInfo()
    {
        if (gameWon)
        {
            LogCheck highestSuccess = CheckWinScores();
            LogCheck worstFailure = CheckLoseScores();

            victoryState.text = "Victory!";

            finalGoodComments.text = "Great work! " + highestSuccess.value + " " + highestSuccess.name;
            finalBadComments.text = "Poor Effort! " + worstFailure.value + " " + worstFailure.name;
        }
        else
        {

            LogCheck highestSuccess = CheckWinScores();
            LogCheck worstFailure = CheckLoseScores();

            victoryState.text = "Defeat!";

            finalGoodComments.text = "Great work! " + highestSuccess.value + " " + highestSuccess.name;
            finalBadComments.text = "Poor Effort! " + worstFailure.value + " " + worstFailure.name;
        }

        sessionScore.text = score.ToString();
        highScore.text = currentHighScore.ToString();
        if (newHighScore)
        {
            highScore.color = Color.green;
        }
        

    }

    LogCheck CheckWinScores()
    {
        LogCheck[] checks = new LogCheck[9];

        checks[0] = new LogCheck("Delivered Coffees", LogManager.log.coffeesDelivered);
        checks[1] = new LogCheck("Filters Changed", LogManager.log.filtersChanged);
        checks[2] = new LogCheck("Toilet Fixed", LogManager.log.toiletsFixed);
        checks[3] = new LogCheck("Fires Extinguished", LogManager.log.firesExtinguished);
        checks[4] = new LogCheck("Delivered Boxes", LogManager.log.boxesDelivered);
        checks[5] = new LogCheck("Mainframe Resets", LogManager.log.serversReset);
        checks[6] = new LogCheck("Printers UnJammed", LogManager.log.printersUnjammed);
        checks[7] = new LogCheck("Blue Screens Fixed", LogManager.log.BSODsFixed);
        checks[8] = new LogCheck("Changed Light Bulbs", LogManager.log.lightBulbsChanged);


        int index = 0;
        int winner = 0;

        for (int i = 0; i < checks.Length; i++)
        {
            if(checks[i].value > winner)
            {
                index = i;
                winner = checks[i].value;
            }
        }

        return checks[index];

    }

    LogCheck CheckLoseScores()
    {
        LogCheck[] checks = new LogCheck[9];

        checks[0] = new LogCheck("Failed Coffees", LogManager.log.thirstyPeople);
        checks[1] = new LogCheck("Filters Unchanged", LogManager.log.filtersUnchanged);
        checks[2] = new LogCheck("Toilet Still Leaking", LogManager.log.toiletsStillBroken);
        checks[3] = new LogCheck("Fires Burning", LogManager.log.firesStillBurning);
        checks[4] = new LogCheck("Boxes Undelivered", LogManager.log.boxesUndelivered);
        checks[5] = new LogCheck("Mainframe Down", LogManager.log.serversDown);
        checks[6] = new LogCheck("Printers Jammed", LogManager.log.printersJammed);
        checks[7] = new LogCheck("Blue Screens", LogManager.log.BSODsUnresolved);
        checks[8] = new LogCheck("Flickering Light Bulbs", LogManager.log.lightsStillFlickering);


        int index = 0;
        int winner = 0;

        for (int i = 0; i < checks.Length; i++)
        {
            if (checks[i].value > winner)
            {
                index = i;
                winner = checks[i].value;
            }
        }

        return checks[index];
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}

public class LogCheck
{
    public string name;
    public int value;

    public LogCheck (string s , int n)
    {
        name = s;
        value = n;
    }
}
