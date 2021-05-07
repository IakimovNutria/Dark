using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : Interaction
{
    public string nextScene;
    private bool lightActive;
    // Start is called before the first frame update
    void Start()
    {
        var canvas = GameObject.FindGameObjectWithTag("Canvas");
        enterText = canvas.transform.GetChild(3).gameObject;
        light = gameObject.transform.GetChild(0).gameObject;
        canObjectBeInteracted = true;
        light.SetActive(false);
        enterText.SetActive(false);
        SceneLoad.currentScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerReadyToInteract && Input.GetKeyDown("space"))
        {
            SceneLoad.prevScene = SceneLoad.currentScene;
            SceneLoad.currentScene = nextScene;
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Player"));
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Flashlight"));
            SceneManager.LoadScene(nextScene);
        }
    }
}
