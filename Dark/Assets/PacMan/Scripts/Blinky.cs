using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;
using Color = UnityEngine.Color;

public class Blinky : MonoBehaviour
{
    private Mario mario;
    private const float Speed = 0.8f; // значение от 0 до 1 
    private int countBound;
    private Score score;
    private int count;
    public Tilemap tilemap;
    private bool[,] maze;
    private Collider2D mazeCollider;
    private static readonly Color AttackColor = Color.white;
    private static readonly Color DefendColor = Color.blue;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        GetMaze();
        mario = (Mario) FindObjectOfType(typeof(Mario));
        countBound = (int) ((1 - Speed) * 100);
        score = (Score) FindObjectOfType(typeof(Score));
        mazeCollider = (Collider2D)tilemap.GetComponent(typeof(Collider2D));
    }

    private void FixedUpdate()
    {
        spriteRenderer.color = mario.IsEnergized ? DefendColor : AttackColor;
        
        count++;
        if (count != countBound) return;
        count = 0;
        var positionInTilemap = gameObject.GetPositionInTilemap(tilemap);
        var marioPositionInTilemap = mario.gameObject.GetPositionInTilemap(tilemap);

        var bestWay = Algorithms.FindBestWay(maze,
            new Point((int) positionInTilemap.x, (int) positionInTilemap.y),
            new Point((int) marioPositionInTilemap.x, (int) marioPositionInTilemap.y));

        var path = new Stack<Point>();

        while (bestWay != null)
        {
            path.Push(bestWay.Value);
            bestWay = bestWay.Previous;
        }

        if (path.Count <= 1) return;

        var currentPos = path.Pop();
        var nextPos = path.Peek();
        var translation = new Vector2(nextPos.X - currentPos.X, nextPos.Y - currentPos.Y);
        if (mario.IsEnergized && gameObject.IsValid(-translation, mazeCollider))
            transform.Translate(-translation);
        else 
            transform.Translate(translation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (mario.IsEnergized)
        {
            Destroy(gameObject);
            score.UpdateScore(100);
        }
        else
            Destroy(mario.gameObject);
    }

    private void GetMaze()
    {
        var bounds = tilemap.cellBounds;
        maze = new bool[bounds.size.x, bounds.size.y];
        var posCounter = 0;

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos))
                maze[posCounter % bounds.size.x, posCounter / bounds.size.x] = true;
            posCounter++;
        }
    }   
}
