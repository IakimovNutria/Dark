using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    private bool isGamePaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
                PauseGame();
            else
                ResumeGame();
        }
    }
    public void PauseGame()
    {
        GameManager.GM.FreezeGame();
        settingsPanel.SetActive(true);
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        settingsPanel.SetActive(false);
        GameManager.GM.ResumeGame();
    }
}
