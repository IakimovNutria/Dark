using UnityEngine;

public class FollowedToPlayer : MonoBehaviour
{
    private Transform player;

    private void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update ()
    {
        var playerPosition = player.position;
        var followed = transform;
        followed.position = new Vector3(playerPosition.x, playerPosition.y, followed.position.z);
    }
}

