using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject LoadingBar;

    private void Awake()
    {
    }
    public void StartGame()
    {
        LoadingBar.SetActive(true);
        StartCoroutine(LoadStartScene());
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
