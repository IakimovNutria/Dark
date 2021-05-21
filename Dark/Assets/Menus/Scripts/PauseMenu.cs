using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject buttons;
    private bool isGamePaused;
    private bool areSettingsOpened;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
                ResumeGame();
            else
                if (!GameManager.GM.isGameFreezed)
                    PauseGame();
        }
    }
    public void PauseGame()
    {
        GameManager.GM.FreezeGame();
        pausePanel.SetActive(true);
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        if (areSettingsOpened)
            CloseSettings();
        pausePanel.SetActive(false);
        GameManager.GM.ResumeGame();
    }

    public void OpenSettings()
    {
        settingsPanel.GetComponent<Settings>().SetSettings();
        buttons.SetActive(false);
        settingsPanel.SetActive(true);
        areSettingsOpened = true;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        buttons.SetActive(true);
        areSettingsOpened = false;
    }

    public void BackToMainMenu()
    {
        GameManager.GM.ResetGame();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
