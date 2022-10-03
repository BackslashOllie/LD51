using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using System;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    bool gamePaused = false;

    public Image pausePanel;
    public Image optionsPanel;
    public Image pauseMenuPanelCover;
    public Image pauseMenuBlockerPanel;

    void OnEnable()
    {
        Time.timeScale = 1;
        StarterAssetsInputs.OnPauseMenuPressed += PauseButtonPressed;
    }

    public void PauseButtonPressed()
    {
        Debug.Log("Pause Pressed");
        if (!gamePaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            StarterAssetsInputs.Instance.DisableMouseLook();
            PauseGame();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            StarterAssetsInputs.Instance.EnableMouseLook();
            UnPauseGame();

        }
    }
    public void PauseGame()
    {
        OpenPauseMenu();
        Time.timeScale = 0;

    }

    public void ResumeButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StarterAssetsInputs.Instance.EnableMouseLook();
        UnPauseGame();
    }

    private void OpenPauseMenu()
    {
        pauseMenuBlockerPanel.gameObject.SetActive(true);
        pausePanel.gameObject.SetActive(true);
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        ClosePauseMenu();
    }

    private void ClosePauseMenu()
    {
        optionsPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        pauseMenuBlockerPanel.gameObject.SetActive(false);
    }

    public void OptionsMenu()
    {
        optionsPanel.gameObject.SetActive(true);
        DisablePauseMenuPanel();
    }

    private void DisablePauseMenuPanel()
    {
        pauseMenuPanelCover.gameObject.SetActive(true);
    }

    public void HideOptionsMenu()
    {
        optionsPanel.gameObject.SetActive(false);
        pauseMenuPanelCover.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    void OnDisable()
    {
        StarterAssetsInputs.OnPauseMenuPressed -= PauseButtonPressed;
    }
}
