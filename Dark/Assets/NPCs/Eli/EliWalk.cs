using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliWalk : AliveEntity
{
    private AliveEntity aliveEntity;
    private float speed = 0.2f;
    public Vector2 destination;
    public string storyBoolToChange;
    public bool isEliGoToBed;
    
    private new void Start()
    {
        if (!GameManager.GM.StoryBools["isPlayerHelpEli"])
            gameObject.SetActive(false);
        aliveEntity = this;
        aliveEntity.Start();
    }

    private new void Update()
    {
        aliveEntity.Update();
        if (Math.Abs(body.position.x - destination.x) < 10e-2 && Math.Abs(body.position.y - destination.y) < 10e-2)
        {
            body.constraints = RigidbodyConstraints2D.FreezeAll;
            if (isEliGoToBed)
                animator.Play("EliBackStandAnimation");
            else 
                aliveEntity.SetHealth(0);
            if (!string.IsNullOrEmpty(storyBoolToChange)) 
                GameManager.GM.ChangeStoryBool(storyBoolToChange, true);
        }
    }

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
