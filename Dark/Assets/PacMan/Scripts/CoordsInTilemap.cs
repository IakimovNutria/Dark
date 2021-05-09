using System;
using UnityEngine;
using UnityEngine.Tilemaps;
 
public static class GameObjectExtension
{
    public static Vector2 GetPositionInTilemap(this GameObject obj, Tilemap tilemap)
    {
        var position = obj.transform.position;
        var x = position.x;
        var y = position.y;
        var positionInTilemap = tilemap.GetCellCenterLocal(new Vector3Int((int) Math.Floor(x),
            (int) Math.Floor(y), 0));
        positionInTilemap -= tilemap.origin;
        positionInTilemap.x = (int)Math.Floor(positionInTilemap.x);
        positionInTilemap.y = (int)Math.Floor(positionInTilemap.y);
        return new Vector2(positionInTilemap.x, positionInTilemap.y);
    }
}

public static class TilemapCoords
{
    public static Vector2 GetWorldPositionFromTilemap(Tilemap tilemap, Vector2 pos)
    {
        var origin = tilemap.origin;
        pos += new Vector2(origin.x, origin.y);
        return new Vector2(pos.x, pos.y);
    }
}