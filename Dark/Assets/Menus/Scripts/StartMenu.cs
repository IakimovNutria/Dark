using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject MenuButtons;
    public GameObject SettingsPanel;
    public GameObject DifficultButtons;
    public void StartGame()
    {
        MenuButtons.SetActive(false);
        DifficultButtons.SetActive(true);
    }

    public void OpenSettings()
    {
        SettingsPanel.GetComponent<Settings>().SetSettings();
        MenuButtons.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        SettingsPanel.SetActive(false);
        MenuButtons.SetActive(true);
    }

    public void QuitGame()
    {
        GameManager.GM.ResetGame();
        Application.Quit();
    }
}
