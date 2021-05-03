using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AliveEntity
{
    public GameObject player;
    public HeatBox heatBox;

    private float maxEnemyHealth = 100;
    private void Start()
    {
        SetMaxHealth(maxEnemyHealth);
    }

    void Update()
    { 
        AliveUpdate();
    }

    protected override void TakeDamage()
    {
        ChangeHealthAmount(-heatBox.GetDamage());
    }
    
    protected override float GetHorizontalVelocity()
    {
        /*var enemyPlayerVector = player.transform.position.x - body.position.x;
        var direction = enemyPlayerVector / Math.Abs(enemyPlayerVector);
        return direction * 2 + 
               body.velocity.x / 10 */
        return (player.transform.position.x - body.position.x) / 2 + 
               body.velocity.x / 10 /*для плавного перехода между анимациями ходьбы*/;
    }

    protected override float GetVerticalVelocity()
    {
        return (player.transform.position.y - body.position.y) / 2 + 
               body.velocity.y / 10 /*для плавного перехода между анимациями ходьбы*/;
    }
}
