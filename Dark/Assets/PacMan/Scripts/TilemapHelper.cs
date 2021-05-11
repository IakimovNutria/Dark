using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;
 
public static class GameObjectExtension
{
    public static Vector2 GetPositionInTilemap(this GameObject obj, Tilemap tilemap)
    {
        var moveToMaze = new Vector2(-1, 10);
        var position = obj.transform.position;
        var x = position.x;
        var y = position.y;
        var positionInTilemap = tilemap.GetCellCenterLocal(new Vector3Int((int) Math.Floor(x),
            (int) Math.Floor(y), 0));
        positionInTilemap -= tilemap.origin;
        positionInTilemap.x = (int)Math.Floor(positionInTilemap.x);
        positionInTilemap.y = (int)Math.Floor(positionInTilemap.y);
        return new Vector2(positionInTilemap.x, positionInTilemap.y) + moveToMaze;
    }
    
    public static bool IsValid(this GameObject obj, Vector2 dir, Collider2D mazeCollider)
    {
        Vector2 pos = obj.transform.position;
        var hit = Physics2D.Linecast(pos + dir, pos);
        return hit.collider != mazeCollider;
    }
}