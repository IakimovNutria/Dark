using System;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        var entity = col.GetComponent<Transform>();
        var position = entity.position;
        position = new Vector3((float) Math.Round(-position.x), position.y, position.z);
        entity.position = position;
        
    }
}