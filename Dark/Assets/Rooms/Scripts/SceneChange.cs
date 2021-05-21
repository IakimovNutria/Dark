using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : Interaction
{
    public string nextScene;

    // Start is called before the first frame update
    public void Start()
    {
        InteractionInitialize(3);
    }

    // Update is called once per frame
    public void Update()
    {
        if (isPlayerReadyToInteract && ActivateCondition() && nextScene != "")
            ChangeScene();
    }

    private void ChangeScene()
    {
        SceneLoadManager.prevSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Flashlight"));
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }

    protected override bool ActivateCondition()
    {
        return Input.GetKeyDown(GameManager.GM.KeyObgectsInteraction);
    }
}
