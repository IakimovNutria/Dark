using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    public float speed = 0.4f;
    private Vector2 destination = Vector2.zero;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    public Collider2D mazeCollider;
    private static readonly int IsWalkLeft = Animator.StringToHash("isWalkLeft");
    private static readonly int IsWalkRight = Animator.StringToHash("isWalkRight");
    private static readonly int IsWalk = Animator.StringToHash("isWalk");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        destination = transform.position;
    }

    private void FixedUpdate()
    {
        if (Geometry.GetLength(transform.position.x, transform.position.y,
            destination.x, destination.y) > 5)
            destination = transform.position;
        var p = Vector2.MoveTowards(transform.position, destination, speed);
        _rigidbody2D.MovePosition(p);

        if ((Vector2) transform.position == destination)
        {
            if (Input.GetKey(Player.Player.KeyUp) && Valid(Vector2.up))
                destination = (Vector2) transform.position + Vector2.up;
            if (Input.GetKey(Player.Player.KeyRight) && Valid(Vector2.right))
                destination = (Vector2) transform.position + Vector2.right;
            if (Input.GetKey(Player.Player.KeyDown) && Valid(-Vector2.up))
                destination = (Vector2) transform.position - Vector2.up;
            if (Input.GetKey(Player.Player.KeyLeft) && Valid(-Vector2.right))
                destination = (Vector2) transform.position - Vector2.right;
        }
        var dir = destination - (Vector2)transform.position;
        _animator.SetBool(IsWalkLeft, dir.x < -0.1);
        _animator.SetBool(IsWalkRight, dir.x > 0.1);
        _animator.SetBool(IsWalk, dir.x > 0.1 || dir.y > 0.1 || dir.x < -0.1 || dir.y < -0.1);
    }

    private bool Valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        var hit = Physics2D.Linecast(pos + dir, pos);
        return hit.collider != mazeCollider;
    }
}
    
