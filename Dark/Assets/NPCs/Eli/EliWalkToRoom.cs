using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliWalkToRoom : AliveEntity
{
    private AliveEntity aliveEntity;
    private float speed = 0.2f;
    private Vector2 destination = new Vector2(-5.35f, -2.51f);
    private void Start()
    {
        aliveEntity = this;
    }

    private new void Update()
    {
        aliveEntity.Update();
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
