using System;
using UnityEngine;

public class EliWalk : Walk
{
    public string storyBoolToChange;
    public bool isEliGoToBed;
    
    private new void Start()
    {
        speed = 0.2f;
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
}
