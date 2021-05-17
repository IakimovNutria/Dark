using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AliveEntity
{
    private Transform playerTransform;
    private float damageRadius = 0.5f;
    private float maxEnemyHealth = 100;
    private Player player;

    private void Start()
    {
        var playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerGameObject.transform;
        player = playerGameObject.GetComponent<Player>();
        
        SetMaxHealth(maxEnemyHealth);
    }

    void Update()
    { 
        AliveUpdate();
        DamagePlayer();
    }

    protected override float GetHorizontalVelocity()
    {
        /*var enemyPlayerVector = playerTransform.transform.position.x - body.position.x;
        var direction = enemyPlayerVector / Math.Abs(enemyPlayerVector);
        return direction * 2 + 
               body.velocity.x / 10 */
        return (playerTransform.position.x - body.position.x) / 2 + 
               body.velocity.x / 10 /*для плавного перехода между анимациями ходьбы*/;
    }

    protected override float GetVerticalVelocity()
    {
        return (playerTransform.position.y - body.position.y) / 2 + 
               body.velocity.y / 10 /*для плавного перехода между анимациями ходьбы*/;
    }

    private void DamagePlayer()
    {
        var enemyPosition = body.position;
        var playerPosition = playerTransform.position;
        var length = Geometry.GetLength(enemyPosition.x, enemyPosition.y, 
            playerPosition.x, playerPosition.y);
        if (length < damageRadius)
            player.TakeDamage(0.75f + length);
    }
}
