using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : Interaction
{
    public string nextScene;
    public static string prevScene;
    public static string currentScene;
    private GameObject light;
    private bool lightActive;
    // Start is called before the first frame update
    void Start()
    {
        light = GameObject.FindGameObjectWithTag("LeftLight");
        light.SetActive(false);
        currentScene = SceneManager.GetActiveScene().name;
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
            prevScene = currentScene;
            SceneManager.LoadScene(nextScene);
        }
    }
}
