using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Algorithms
{
    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        public readonly T Value;
        public readonly SinglyLinkedList<T> Previous;
        private readonly int length;

        public SinglyLinkedList(T value, SinglyLinkedList<T> previous = null)
        {
            Value = value;
            Previous = previous;
            length = previous?.length + 1 ?? 1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            yield return Value;
            var pathItem = Previous;
            while (pathItem != null)
            {
                yield return pathItem.Value;
                pathItem = pathItem.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public static SinglyLinkedList<Point> FindBestWay(bool[,] maze, Point start, Point finish)
    {
        var pointsToVisit = new Queue<SinglyLinkedList<Point>>();
        var visited = new HashSet<Point>();
        pointsToVisit.Enqueue(new SinglyLinkedList<Point>(start));
        visited.Add(start);
        while (pointsToVisit.Count != 0)
        {
            var node = pointsToVisit.Dequeue();
            var point = node.Value;
            if (point.X < 0 || point.X >= maze.GetLength(0) || 
                point.Y < 0 || point.Y >= maze.GetLength(1) || 
                !maze[point.X, point.Y]) continue;
            
            if (point == finish) return node;
            for (var dy = -1; dy <= 1; dy++)
            for (var dx = -1; dx <= 1; dx++)
            {
                if (dx != 0 && dy != 0) continue;
                var pointToVisit = new Point {X = point.X + dx, Y = point.Y + dy};
                if (visited.Contains(pointToVisit)) continue;
                pointsToVisit.Enqueue(new SinglyLinkedList<Point>(pointToVisit, node));
                visited.Add(pointToVisit);
            }
        }
        return null;
    }
}
