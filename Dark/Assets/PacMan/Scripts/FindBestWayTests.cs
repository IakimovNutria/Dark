using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;

[TestFixture]
public class FindBestWayTests
{
    [Test]
    public void ReturnNullWhenNoWay()
    {
        var textMaze = new[]
        {
            "S ",
            "##",
            "F "
        };

        var bestWay = FindBestWayWithTextMap(textMaze);

        Assert.IsNull(bestWay);
    }

    [Test]
    public void ReturnBestWayOnEmptyMaze()
    {
        var textMaze = new[]
        {
            "S ",
            "F "
        };
        var bestWay = FindBestWayWithTextMap(textMaze);
        Assert.Equals(bestWay, MakeSinglyLinkedList(new [] {new Point(0,0), new Point(0,1)}));
    }

    [Test]
    public void ReturnCorrectPaths_OnSimpleDungeon()
    {
        var textMaze = new[]
        {
            "S #",
            "# #",
            "F  "
        };
        var bestWay = FindBestWayWithTextMap(textMaze);

    }

    [Test]
    public void Return_ShortestPath1()
    {
        var textMaze = new[]
        {
            "   ",
            " S ",
            " F "
        };
        var bestWay = FindBestWayWithTextMap(textMaze);

    }

    [Test]
    public void Return_ShortestPath2()
    {
        var textMaze = new[]
        {
            "F ",
            " S",
        };
        var bestWay = FindBestWayWithTextMap(textMaze);

    }

    private Algorithms.SinglyLinkedList<Point> FindBestWayWithTextMap(IReadOnlyList<string> textMaze)
    {
        var start = new Point();
        var finish = new Point();
        var maze = new bool[textMaze.Count, textMaze[0].Length];
        for (var i = 0; i < textMaze.Count; i++)
        {
            for (var j = 0; j < textMaze[0].Length; j++)
            {
                switch (textMaze[i][j])
                {
                    case 'S':
                        start = new Point(i, j);
                        break;
                    case 'F':
                        finish = new Point(i, j);
                        break;
                }

                maze[i, j] = textMaze[i][j] != '#';
            }
        }

        return Algorithms.FindBestWay(maze, start, finish);
    }

    private Algorithms.SinglyLinkedList<Point> MakeSinglyLinkedList(IReadOnlyList<Point> points)
    {
        if (points.Count == 0)
            return null;
        
        var singlyLinkedList = new Algorithms.SinglyLinkedList<Point>(points[0]);
        
        for (var i = 1; i < points.Count; i++)
            singlyLinkedList = new Algorithms.SinglyLinkedList<Point>(points[i], singlyLinkedList);

        return singlyLinkedList;
    }
}