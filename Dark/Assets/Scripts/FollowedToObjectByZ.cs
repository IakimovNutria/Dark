using UnityEngine;

public class FollowedToObjectByZ : MonoBehaviour
{
    public new GameObject gameObject;
    private void Update()
    {
        var objectPosition = gameObject.transform.position;
        var followed = transform;
        var followedPosition = followed.position;
        followed.position = new Vector3(followedPosition.x, followedPosition.y, objectPosition.z);
    }
}