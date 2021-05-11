using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    private float speed = 0.1f;
    public bool IsEnergized { get; private set; }
    private const int EnergizedTime = 500;
    private int currentEnergizedTime;
    public Vector2 destination = Vector2.zero;
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
        if (currentEnergizedTime != 0)
            currentEnergizedTime--;
        else
        {
            IsEnergized = false;
            currentEnergizedTime = EnergizedTime;
        }
        if (Geometry.GetLength(transform.position.x, transform.position.y,
            destination.x, destination.y) > 5)
            destination = transform.position;
        var p = Vector2.MoveTowards(transform.position, destination, speed);
        _rigidbody2D.MovePosition(p);

        if ((Vector2) transform.position == destination)
        {
            if (Input.GetKey(Player.Player.KeyUp) && gameObject.IsValid(Vector2.up, mazeCollider))
                destination = (Vector2) transform.position + Vector2.up;
            if (Input.GetKey(Player.Player.KeyRight) && gameObject.IsValid(Vector2.right, mazeCollider))
                destination = (Vector2) transform.position + Vector2.right;
            if (Input.GetKey(Player.Player.KeyDown) && gameObject.IsValid(-Vector2.up, mazeCollider))
                destination = (Vector2) transform.position - Vector2.up;
            if (Input.GetKey(Player.Player.KeyLeft) && gameObject.IsValid(-Vector2.right, mazeCollider))
                destination = (Vector2) transform.position - Vector2.right;
        }
        var dir = destination - (Vector2)transform.position;
        _animator.SetBool(IsWalkLeft, dir.x < -0.1);
        _animator.SetBool(IsWalkRight, dir.x > 0.1);
        _animator.SetBool(IsWalk, dir.x > 0.1 || dir.y > 0.1 || dir.x < -0.1 || dir.y < -0.1);
    }

    public void Energize()
    {
        IsEnergized = true;
    }
}
    
