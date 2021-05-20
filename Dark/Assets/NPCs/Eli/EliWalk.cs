using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliWalk : AliveEntity
{
    private AliveEntity aliveEntity;
    private float speed = 0.2f;
    public Vector2 destination;  
    private new void Start()
    {
        aliveEntity = this;
        aliveEntity.Start();
    }

    private new void Update()
    {
        aliveEntity.Update();
        if (Math.Abs(body.position.x - destination.x) < 10e-2 && Math.Abs(body.position.y - destination.y) < 10e-2)
        {
            GameManager.GM.ChangeStoryBool("isEliReachedRoom");
        }
    }

    protected override float GetHorizontalVelocity()
    {
        var bodyPosition = body.transform.position;
        if (Math.Abs(bodyPosition.x - destination.x) < 10e-2)
            return 0;
        return bodyPosition.x < destination.x ? speed : -speed;
    }

    protected override float GetVerticalVelocity()
    {
        var bodyPosition = body.transform.position;
        if (Math.Abs(bodyPosition.y - destination.y) < 10e-2)
            return 0;
        return bodyPosition.y < destination.y ? speed : -speed;
    }
}
