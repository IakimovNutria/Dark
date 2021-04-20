using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D body;
    private Animator animator;
    private Transform player;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var previousVelocity = body.velocity;
        var playerPosition = player.position;
        var enemyPosition = body.position;
        var horizontalVelocity = (playerPosition.x - enemyPosition.x) / 2 + 
                                 previousVelocity.x / 10; //для плавного перехода между анимациями ходьбы
        var verticalVelocity = (playerPosition.y - enemyPosition.y) / 2 + 
                               previousVelocity.y / 10; //для плавного перехода между анимациями ходьбы
        
        animator.SetBool("isWalk", !(horizontalVelocity == 0 && verticalVelocity == 0));
            
        animator.SetBool("isWalkLeft", horizontalVelocity < 0);
        animator.SetBool("isWalkBack", verticalVelocity < 0 && horizontalVelocity == 0);
        animator.SetBool("isWalkRight", horizontalVelocity > 0);
        animator.SetBool("isWalkFront", verticalVelocity > 0 && horizontalVelocity == 0);
            
        body.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }
}
