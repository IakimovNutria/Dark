using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultChoice : MonoBehaviour
{
    public GameObject EasyLoadingBar;
    public GameObject MiddleLoadingBar;
    public GameObject HardLoadingBar;
    
    public void SetEasyDifficult()
    {
        GameManager.GM.SetEasyDifficult();
        EasyLoadingBar.SetActive(true);
        StartCoroutine(LoadStartScene(EasyLoadingBar));
    }

    public void SetMiddleDifficult()
    {
        GameManager.GM.SetMiddleDifficult();
        MiddleLoadingBar.SetActive(true);
        StartCoroutine(LoadStartScene(MiddleLoadingBar));
    }
    
    public void SetHardDifficult()
    {
        GameManager.GM.SetHardDifficult();
        HardLoadingBar.SetActive(true);
        StartCoroutine(LoadStartScene(HardLoadingBar));
    }
    
    private IEnumerator LoadStartScene(GameObject loadingBar)
    {
        var operation = SceneManager.LoadSceneAsync("StartingRoom");
        var slider = loadingBar.GetComponent<Slider>();

        while (!operation.isDone)
        {
            var progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}