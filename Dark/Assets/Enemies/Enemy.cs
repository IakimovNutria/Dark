using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AliveEntity
{
    // Start is called before the first frame update
    private GameObject player;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    { 
        Move();
    }

    protected override float GetHorizontalVelocity()
    {
        return (player.transform.position.x - body.position.x) / 2 + 
               body.velocity.x / 10 /*для плавного перехода между анимациями ходьбы*/;
    }

    protected override float GetVerticalVelocity()
    {
        return (player.transform.position.y - body.position.y) / 2 + 
               body.velocity.y / 10 /*для плавного перехода между анимациями ходьбы*/;
    }
}
