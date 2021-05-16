using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static bool firstRoomChestVisited;
    public GameObject player;
    public Component flashlightScript;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        flashlightScript = player.GetComponent("Flashlight");
    }

    public void FreezeGame()
    {
        Time.timeScale = 0;
        flashlightScript.
    }

    public void ResumeGame() => Time.timeScale = 1;
}
