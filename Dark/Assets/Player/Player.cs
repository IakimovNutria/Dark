using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class Player : AliveEntity
{
    public static Player Instance;
    private float speed = 1;
    private float maxPlayerHealth = 1000;
    private AliveEntity aliveEntity;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
            Destroy(gameObject);

        aliveEntity = this;
        SetMaxHealth(maxPlayerHealth);
    }

    private new void Update()
    {
        aliveEntity.Update();
    }
    protected override float GetHorizontalVelocity()
    {
        return (Input.GetKey(GameManager.GM.KeyRight) ? speed : 0) + 
               (Input.GetKey(GameManager.GM.KeyLeft) ? -speed : 0) +
                   body.velocity.x / 10 /*для плавного перехода между анимациями ходьбы*/;
    }

    protected override float GetVerticalVelocity()
    {
        return (Input.GetKey(GameManager.GM.KeyUp) ? speed : 0) + 
               (Input.GetKey(GameManager.GM.KeyDown) ? -speed : 0) +
                   body.velocity.y / 10 /*для плавного перехода между анимациями ходьбы*/;
    }
}