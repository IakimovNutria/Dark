using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : Interaction
{
    public string nextScene;

    private SceneLoadManager sceneLoadManager;
    
    public void Start()
    {
        sceneLoadManager = FindObjectOfType<SceneLoadManager>();
        InteractionInitialize(3);
    }
    
    public void Update()
    {
        if (isPlayerReadyToInteract && ActivateCondition() && nextScene != "")
        {
            sceneLoadManager.Save();
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        SceneLoadManager.prevSceneIndex = SceneManager.GetActiveScene().buildIndex;
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
