using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationChange : ActivationCode
{
    void Start()
    {
        SetKeysSequence(new List<string>{"q","w","e","r"});
    }
    void Update()
    {
        ActivationCodeUpdate();
    }
    
    private void ChangeScene()
    {
        SceneLoadManager.prevSceneIndex = 2;
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Flashlight"));
        UnityEngine.SceneManagement.SceneManager.LoadScene("MalcolmRoom");
    }
    
    protected override void Activate()
    {
        ChangeScene();
    }
}
