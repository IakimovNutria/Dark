using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static bool firstRoomChestVisited;
    public static GameObject flashlight;

    private void Start()
    {
        flashlight = GameObject.FindGameObjectWithTag("Flashlight");
    }

    public void FreezeGame()
    {
        Time.timeScale = 0;
        flashlight.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        flashlight.SetActive(true);
    }
}
