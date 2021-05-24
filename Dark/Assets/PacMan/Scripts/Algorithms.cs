using System.Collections;
using System.Collections.Generic;

public static class Algorithms
{
    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        public readonly T Value;
        public readonly SinglyLinkedList<T> Previous;
        public readonly int Length;

        public SinglyLinkedList(T value, SinglyLinkedList<T> previous = null)
        {
            Value = value;
            Previous = previous;
            Length = previous?.Length + 1 ?? 1;
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
        if (maze is null)
            return null;
        if (finish.X > maze.GetLength(0) - 1 || finish.Y > maze.GetLength(1) - 1 ||
            start.X > maze.GetLength(0) - 1 || start.Y > maze.GetLength(1) - 1 ||
            start.X < 0 || start.Y < 0 || finish.Y < 0 || finish.X < 0 
            || !maze[start.X, start.Y] || !maze[finish.X, finish.Y])
            return null;
        var pointsToVisit = new Queue<SinglyLinkedList<Point>>();
        var visited = new HashSet<Point>();
        pointsToVisit.Enqueue(new SinglyLinkedList<Point>(start));
        visited.Add(start);
        while (pointsToVisit.Count != 0)
        {
            var node = pointsToVisit.Dequeue();
            var point = node.Value;
            
            if (point.Equals(finish)) return node;
            for (var dy = -1; dy <= 1; dy++)
            for (var dx = -1; dx <= 1; dx++)
            {
                if (dx != 0 && dy != 0 || dx == 0 && dy == 0) continue;
                var pointToVisit = new Point(point.X + dx, point.Y + dy);
                if (visited.Contains(pointToVisit) || !IsPointAvailable(pointToVisit, maze)) continue;
                pointsToVisit.Enqueue(new SinglyLinkedList<Point>(pointToVisit, node));
                visited.Add(pointToVisit);
            }
        }
        return null;
    }

    private static bool IsPointAvailable(Point point, bool[,] maze)
    {
        return point.X >= 0 && point.X < maze.GetLength(0) &&
               point.Y >= 0 && point.Y < maze.GetLength(1) &&
               maze[point.X, point.Y];
    }
}

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return "(" + X + ", " + Y + ")";
    }
}