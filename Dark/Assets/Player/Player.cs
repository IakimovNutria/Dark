using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Player : AliveEntity
    {
        private GameObject[] enemies;

        // не const, чтобы можно было менять управление в настройках
        
        //не const чтобы можно было менять сложность в настройках
        private float speed = 1;
        private float maxPlayerHealth = 1000;
        private float damageRadius = 0.5f;
        
        private void Start()
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            SetMaxHealth(maxPlayerHealth);
        }

        private void Update()
        {
            AliveUpdate();
        }

        protected override float GetHorizontalVelocity()
        {
            return (Input.GetKey(GameManager.GM.KeyRight) ? speed : 0) + (Input.GetKey(GameManager.GM.KeyLeft) ? -speed : 0) +
                   body.velocity.x / 10 /*для плавного перехода между анимациями ходьбы*/;
        }

        protected override float GetVerticalVelocity()
        {
            return (Input.GetKey(GameManager.GM.KeyUp) ? speed : 0) + (Input.GetKey(GameManager.GM.KeyDown) ? -speed : 0) +
                   body.velocity.y / 10 /*для плавного перехода между анимациями ходьбы*/;
        }

        protected override void TakeDamage()
        {
            foreach (var enemy in enemies)
            {
                if (!enemy.CompareTag("Enemy"))
                    return;
                var enemyPosition = enemy.transform.position;
                var playerPosition = body.position;
                var length = Geometry.GetLength(enemyPosition.x, enemyPosition.y, 
                    playerPosition.x, playerPosition.y);
                if (length < damageRadius)
                {
                    ChangeHealthAmount(-0.75f + length);
                }
            }
        }
    }
}