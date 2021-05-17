using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused;
    //public Button pauseButton;
    public GameObject pauseMenuUI;
    public GameManager sceneController;
    public bool flag;

    private void Start()
    {
        pauseMenuUI = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenuUI.SetActive(false);
        sceneController = this.GetComponent<GameManager>();
        //pauseButton = GameObject.FindGameObjectWithTag("PauseButton").GetComponent<Button>();
        //pauseButton.onClick.AddListener(PauseGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }

    public void PauseGame()
    {
        flag = true;
        if (isGamePaused)
            Resume();
        else
        {
            pauseMenuUI.SetActive(true);
            sceneController.FreezeGame();
            isGamePaused = true;
        }
    }

    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
    }
}
