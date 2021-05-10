using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
    public Tilemap tilemap;
    protected bool[,] maze;
    public Collider2D mazeCollider;
    protected void GhostStart ()
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

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
