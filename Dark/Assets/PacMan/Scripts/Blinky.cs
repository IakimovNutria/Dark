using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditorInternal;
using UnityEngine;

public class Blinky : Ghost
{
    private GameObject mario;
    private float speed = 1;
    private Vector2 destination = Vector2.zero;
    private Vector2 moveToMaze;
    private Vector2 step;
    private int countBound = 100;
    public Rigidbody2D _rigidbody2D;

    private int count;
    // Start is called before the first frame update
    void Start()
    {
        GhostStart();
        mario = GameObject.FindGameObjectWithTag("Player");
        moveToMaze = new Vector2(0, 10);
        step = new Vector2(0, 0);
        destination = gameObject.transform.position;
    }
    private void FixedUpdate()
    {
        count++;
        Vector2 position = transform.position;
        Algorithms.SinglyLinkedList<Point> bestWay = null;
        if (count == countBound)
        {
            var positionInTilemap = gameObject.GetPositionInTilemap(tilemap) + moveToMaze;
            var marioPositionInTilemap = mario.GetPositionInTilemap(tilemap) + moveToMaze;
            count = 0;
            bestWay = Algorithms.FindBestWay(maze,
                new Point((int)positionInTilemap.x, (int)positionInTilemap.y),
                new Point((int)marioPositionInTilemap.x, (int)marioPositionInTilemap.y));

            var path = new Stack<Point>();
            while (bestWay != null)
            {
                path.Push(bestWay.Value);
                bestWay = bestWay.Previous;
            }

            step = new Vector2(0, 0);
            if (path.Count > 1)
            {
                var currentPos = path.Pop();
                var nextPos = path.Peek();
                step = new Vector2(nextPos.X - currentPos.X, nextPos.Y - currentPos.Y);
                destination = (Vector2)position + step;
                transform.Translate(step);
            }
        }
    }
    private bool Valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        var hit = Physics2D.Linecast(pos + dir, pos);
        return hit.collider != mazeCollider;
    }
}
