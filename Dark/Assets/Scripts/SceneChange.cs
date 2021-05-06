using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : Interaction
{
    public string nextScene;
    public GameObject light;
    private bool lightActive;
    // Start is called before the first frame update
    void Start()
    {
        var canvas = GameObject.FindGameObjectWithTag("Canvas");
        enterText = canvas.transform.GetChild(3).gameObject;
        light = gameObject.transform.GetChild(0).gameObject;
        light.SetActive(false);
        enterText.SetActive(false);
        SceneController.currentScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAbleToInteract && !lightActive)
        {
            lightActive = true;
            light.SetActive(true);
        }
        if (!isAbleToInteract && lightActive)
        {
            lightActive = false;
            light.SetActive(false);
        }
        if (isAbleToInteract && Input.GetKeyDown("space"))
        {
            SceneController.prevScene = SceneController.currentScene;
            SceneController.currentScene = nextScene;
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Player"));
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Flashlight"));
            SceneManager.LoadScene(nextScene);
        }
    }
}
