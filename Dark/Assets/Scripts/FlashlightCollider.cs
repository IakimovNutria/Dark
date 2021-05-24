using UnityEngine;

public class FlashlightCollider : MonoBehaviour
{
    private Transform Flashlight;

    private void Start()
    {
        Flashlight = GameObject.FindGameObjectWithTag("Flashlight").transform;
    }

    private void Update()
    {
        var flashlightPosition = Flashlight.position;
        var followed = transform;
        var followedPosition = followed.position;
        followed.position = new Vector3(followedPosition.x, followedPosition.y, flashlightPosition.z);
    }
}