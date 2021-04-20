using UnityEngine;

public class Spotlight : MonoBehaviour
{
    private Transform player;

    private void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    private void Update ()
    {
        var playerPosition = player.position;
        var spotlight = transform;
        spotlight.position = new Vector3(playerPosition.x, playerPosition.y, spotlight.position.z);
    }
}

