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
        var allTiles = tilemap.GetTilesBlock(bounds);

        for (var x = 0; x < bounds.size.x; x++)
        for (var y = 0; y < bounds.size.y; y++) 
                maze[x, y] = allTiles[y + x * bounds.size.y] == null;
    }   

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
