using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : Interaction
{
    public string nextScene;
    private SceneSaveManager sceneSaveManager;
    
    private void Start()
    {
        sceneSaveManager = FindObjectOfType<SceneSaveManager>();
        InteractionInitialize();
    }
    
    private void Update()
    {
        if (isPlayerReadyToInteract && (ActivateCondition() || mustNotBeInteraction) 
                                    && nextScene != "" && StoryBoolActivateCondition())
        {
            sceneSaveManager.SaveScene();
            Invoke(nameof(ChangeScene), invokeTime);
        }
    }

    private void ChangeScene()
    {
        GameManager.GM.DoorSound.Play();
        SceneLoadManager.prevSceneIndex = SceneManager.GetActiveScene().buildIndex;
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Flashlight"));
        SceneManager.LoadScene(nextScene);
    }
}

