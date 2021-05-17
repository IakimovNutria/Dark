using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public void OpenSettings()
    {
        GameManager.GM.FreezeGame();
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        GameManager.GM.ResumeGame();
    }
}
