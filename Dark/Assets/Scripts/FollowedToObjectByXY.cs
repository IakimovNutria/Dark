using UnityEngine;

public class FollowedToObjectByXY : MonoBehaviour
{
    public new GameObject gameObject;
    private void Update()
    {
        var objectPosition = gameObject.transform.position;
        var followed = transform;
        followed.position = new Vector3(objectPosition.x, objectPosition.y, followed.position.z);
    }
}
