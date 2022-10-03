using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private bool uiSlide = false;
    public float startDelay = 3f;
    public float uiSlideRate = 300f;
    public Image menuPanel;
    public Image mainMenuPanelCover;
    public Image optionsMenuPanel;
    public Image howToPanel;

    public ObjectSmoothMover companyLogo;
    public ObjectSmoothMover gameTitle;


    public void StartNewGame()
    {
        StartCoroutine(DelayedStart());
        companyLogo.StartMoving();
        gameTitle.StartMoving();
    }

    public void OptionsMenu()
    {
        optionsMenuPanel.gameObject.SetActive(true);
        DisableMainMenu();
    }

    public void HowToPlayMenu()
    {
        howToPanel.gameObject.SetActive(true);
        DisableMainMenu();
    }

    public void HideOptionsMenu()
    {
        optionsMenuPanel.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator DelayedStart()
    {
        uiSlide = true;
        yield return new WaitForSeconds(startDelay);
        SceneManager.LoadScene("Main");
    }

    private void DisableMainMenu()
    {
        mainMenuPanelCover.gameObject.SetActive(true);
    }

    public void EnableMainMenu()
    {
        mainMenuPanelCover.gameObject.SetActive(false);
        optionsMenuPanel.gameObject.SetActive(false);
        howToPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (uiSlide)
        {
            menuPanel.transform.Translate(Vector3.left * uiSlideRate * Time.deltaTime);

        }
    }
}
