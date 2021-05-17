using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoad : MonoBehaviour
{
    public static string prevScene;
    public static string currentScene;

    public Transform player;
    public string leftScene;
    public string rightScene;
    public string topScene;
    public string bottomScene;
    public Vector2 leftStart;
    public Vector2 rightStart;
    public Vector2 topStart;
    public Vector2 bottomStart;

    // Start is called before the first frame update
    void Start()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 1)
        {
            Object.Destroy(players[1]);
            Object.Destroy(GameObject.FindGameObjectsWithTag("MainCamera")[1]);
            Object.Destroy(GameObject.FindGameObjectsWithTag("Canvas")[1]);
            Object.Destroy(GameObject.FindGameObjectsWithTag("Flashlight")[1]);
        }
        player = players[0].transform;
        if (prevScene == rightScene)
            player.position = rightStart;
        if (prevScene == leftScene)
            player.position = leftStart;
        if (prevScene == topScene)
            player.position = topStart;
        if (prevScene == bottomScene)
            player.position = bottomStart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
