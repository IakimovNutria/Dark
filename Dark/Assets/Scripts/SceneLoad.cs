using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoad : SceneSwitch
{
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
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
