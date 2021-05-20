using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject MenuButtons;
    public GameObject SettingsPanel;
    public GameObject LoadingBar;
    public void StartGame()
    {
        LoadingBar.SetActive(true);
        StartCoroutine(LoadStartScene());
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
        Application.Quit();
    }

    private IEnumerator LoadStartScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("StartingRoom");
        var slider = LoadingBar.GetComponent<Slider>();

        while (!operation.isDone)
        {
            var progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
