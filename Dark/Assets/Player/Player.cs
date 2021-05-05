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
        private string keyUp = "w";
        private string keyLeft = "a";
        private string keyDown = "s";
        private string keyRight = "d";
        public string keyDamageLight = "Fire1";
        public string keyHealLight = "Fire2";
        
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
            return (Input.GetKey(keyRight) ? speed : 0) + (Input.GetKey(keyLeft) ? -speed : 0) +
                   body.velocity.x / 10 /*для плавного перехода между анимациями ходьбы*/;
        }

        protected override float GetVerticalVelocity()
        {
            return (Input.GetKey(keyUp) ? speed : 0) + (Input.GetKey(keyDown) ? -speed : 0) +
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
                var length = GetLength(enemyPosition.x, enemyPosition.y, 
                    playerPosition.x, playerPosition.y);
                if (length < damageRadius)
                {
                    ChangeHealthAmount(-0.75f + length);
                }
            }
        }

        private static float GetLength(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
    }
}