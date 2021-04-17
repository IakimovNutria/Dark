using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Player
{
    public class PlayerScript : MonoBehaviour
    {
        // Start is called before the first frame update
        private Rigidbody2D body;
        private Animator animator;
        private string keyUp = "w";
        private string keyLeft = "a";
        private string keyDown = "s";
        private string keyRight = "d";
        private float speed = 1;
        
        private void Start()
        {
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            var previousVelocity = body.velocity;
            var horizontalVelocity = (Input.GetKey(keyRight) ? speed : 0) + (Input.GetKey(keyLeft) ? -speed : 0) + 
                                     previousVelocity.x / 100000; //для плавного перехода между анимациями ходьбы
            var verticalVelocity = (Input.GetKey(keyUp) ? speed : 0) + (Input.GetKey(keyDown) ? -speed : 0) + 
                                     previousVelocity.y / 100000; //для плавного перехода между анимациями ходьбы
            
            animator.SetBool("isWalk", !(horizontalVelocity == 0 && verticalVelocity == 0));
            
            animator.SetBool("isWalkLeft", horizontalVelocity < 0);
            animator.SetBool("isWalkBack", verticalVelocity < 0 && horizontalVelocity == 0);
            animator.SetBool("isWalkRight", horizontalVelocity > 0);
            animator.SetBool("isWalkFront", verticalVelocity > 0 && horizontalVelocity == 0);
            
            body.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        }
    }
}