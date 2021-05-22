using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UlfWalk : Walk
{
    private new void Start()
    {
        speed = 0.5f;
        aliveEntity = this;
        aliveEntity.Start();
    }

    private new void Update()
    {
        aliveEntity.Update();
        
        if (Math.Abs(body.position.x - destination.x) < 10e-2 && Math.Abs(body.position.y - destination.y) < 10e-2)
        {
            body.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.Play("UlfBackStandAnimation");
        }
    }
}
