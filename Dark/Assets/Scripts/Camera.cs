using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform player;

    private void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    private void Update ()
    {
        var playerPosition = player.position;
        var camera = transform;
        camera.position = new Vector3(playerPosition.x, playerPosition.y, camera.position.z);
    }
}

