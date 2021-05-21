using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : Interaction
{
    public string nextScene;

    private void Start()
    {
        InteractionInitialize(3);
    }

    private void Update()
    {
        if (isPlayerReadyToInteract && (ActivateCondition() || mustNotBeInteraction) 
                                    && nextScene != "" && StoryBoolActivateCondition())
            Invoke(nameof(ChangeScene), invokeTime);;
    }

    private void ChangeScene()
    {
        SceneLoad.prevSceneIndex = SceneManager.GetActiveScene().buildIndex;
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Flashlight"));
        SceneManager.LoadScene(nextScene);
    }

    protected override bool ActivateCondition()
    {
        return Input.GetKeyDown(GameManager.GM.KeyObgectsInteraction);
    }
}

