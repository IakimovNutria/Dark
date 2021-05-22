using System;
using UnityEngine;

public class Geometry 
{
    public static float GetLength(Vector2 firstPoint, Vector2 secondPoint)
    {
        return (float)Math.Sqrt(Math.Pow(firstPoint.x - secondPoint.x, 2) + 
                                Math.Pow(firstPoint.y - secondPoint.y, 2));
    }
}
