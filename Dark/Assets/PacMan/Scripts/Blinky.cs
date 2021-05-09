using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditorInternal;
using UnityEngine;

public class Blinky : Ghost
{
    private GameObject mario;
    private float speed = 0.2f;
    private Vector2 destination = Vector2.zero;
    public Rigidbody2D _rigidbody2D;

    private int count;
    // Start is called before the first frame update
    void Start()
    {
        GhostStart();
        mario = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        count++;
        Vector2 position = transform.position;
        Algorithms.SinglyLinkedList<Point> bestWay = null;
        if (count == 100)
        {
            var positionInTilemap = gameObject.GetPositionInTilemap(tilemap);
            var marioPositionInTilemap = mario.GetPositionInTilemap(tilemap);
            count = 0;
            bestWay = Algorithms.FindBestWay(maze, 
                new Point((int) Math.Round(positionInTilemap.x), (int) Math.Round(positionInTilemap.y)),
                new Point((int) Math.Round(marioPositionInTilemap.x), 
                    (int) Math.Round(marioPositionInTilemap.y)));
        }

        var path = new Stack<Point>();
        while (bestWay != null)
        {
            path.Push(bestWay.Value);
            bestWay = bestWay.Previous;
        }
        if (Geometry.GetLength(position.x, position.y,
            destination.x, destination.y) > 5)
            destination = position;
        var p = Vector2.MoveTowards(position, destination, speed);
        _rigidbody2D.MovePosition(p);
        if (position != destination) return;
        try
        {
            path.Pop();
        }
        catch
        {
            return;
        }

        var nextStep = path.Pop();
        destination = position + new Vector2(nextStep.X, nextStep.Y);
    }
    private bool Valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        var hit = Physics2D.Linecast(pos + dir, pos);
        return hit.collider != mazeCollider;
    }
}
