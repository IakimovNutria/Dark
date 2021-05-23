using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : AliveEntity
{
    protected float speed = 0.2f;
    [SerializeField]
    protected Vector2 destination;
    
    protected override Vector2 GetVelocity()
    {
        var bodyPosition = body.transform.position;
        float horizontalVelocity;
        float verticalVelocity;
        if (Math.Abs(bodyPosition.x - destination.x) < 10e-2)
            horizontalVelocity = 0;
        else
            horizontalVelocity = bodyPosition.x < destination.x ? speed : -speed;
        
        if (Math.Abs(bodyPosition.y - destination.y) < 10e-2)
            verticalVelocity = 0;
        else
            verticalVelocity = bodyPosition.y < destination.y ? speed : -speed;
        
        return new Vector2(horizontalVelocity, verticalVelocity);
    }
}
