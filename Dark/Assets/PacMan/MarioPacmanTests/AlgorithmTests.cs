using System.Collections.Generic;
using NUnit.Framework;

public class AlgorithmTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void OneStepPath()
    {
        var maze = new[,]
        {
            { true, true },
            { true, true }
        };
        var start = new Point(0, 0);
        var finish = new Point(0, 1);
        var expectedLength = 2;
        Assert.AreEqual(expectedLength, Algorithms.FindBestWay(maze, start, finish).Length);
    }

    [Test]
    public void GetAroundTheWall()
    {
        var maze = new[,]
        {
            { true, false, true },
            { true, true, true }
        };
        var start = new Point(0, 0);
        var finish = new Point(0, 2);
        Assert.AreEqual(GetSinglyLinkedList(new List<Point> {
            new Point(0,0), new Point(1,0), new Point(1, 1), 
            new Point(1, 2), new Point(0,2)
        }), Algorithms.FindBestWay(maze, start, finish));
        
    }

    [Test]
    public void StayIfNoPaths()
    {
        var maze = new[,]
        {
            { true, false, true },
            { false, true, true }
        };
        var start = new Point(0, 0);
        var finish = new Point(0, 2);
        Assert.IsNull(Algorithms.FindBestWay(maze, start, finish));
    }

    [Test]
    public void FindBestWay()
    {
        var maze = new[,]
        {
            {true, true, true, true},
            {true, false, false, true}, 
            {true, true, true, true}
        };
        var start = new Point(1, 0);
        var finish = new Point(0, 3);
        Assert.AreEqual(GetSinglyLinkedList(
            new List<Point>
            {
                new Point(1,0), new Point(0,0), new Point(0,1), 
                new Point(0,2), new Point(0, 3)
            }), Algorithms.FindBestWay(maze, start, finish));
        
        start = new Point(2, 1);
        Assert.AreEqual(GetSinglyLinkedList(
            new List<Point>
            {
                new Point(2,1), new Point(2,2), new Point(2,3), 
                new Point(1,3), new Point(0, 3)
            }), Algorithms.FindBestWay(maze, start, finish));
    }

    [Test]
    public void ReturnNullWhenIndexOutOfRange()
    {
        var maze = new[,]
        {
            {true, true, true},
            {true, true, true}
        };
        var start = new Point(0, 0);
        var finish = new Point(2, 0);
        Assert.IsNull(Algorithms.FindBestWay(maze, start, finish));
        start = new Point(2,0);
        finish = new Point(0,0);
        Assert.IsNull(Algorithms.FindBestWay(maze,start,finish));
        start = new Point(-1, 0);
        finish = new Point(0,0);
        Assert.IsNull(Algorithms.FindBestWay(maze,start,finish));
        start = new Point(0,0);
        finish = new Point(0,-1);
        Assert.IsNull(Algorithms.FindBestWay(maze,start,finish));
    }

    [Test]
    public void ReturnNullWhenMazeNull()
    {
        Assert.IsNull(Algorithms.FindBestWay(null, new Point(0,0), new Point(0,0)));
    }

    [Test]
    public void ReturnStartWhenStartIsFinish()
    {
        var maze = new [,]
        {
            {true, true},
            {true, true}
        };
        var start = new Point(0,0);
        var finish = new Point(0, 0);
        Assert.AreEqual(new Algorithms.SinglyLinkedList<Point>(new Point(0, 0)),
            Algorithms.FindBestWay(maze, start, finish));
    }

    [Test]
    public void ReturnNullWhenStartOrFinishInWall()
    {
        var maze = new[,]
        {
            {true, true, true},
            {true, false, true},
            {true, true, true}
        };
        var start = new Point(1, 1);
        var finish = new Point(0,0);
        Assert.IsNull(Algorithms.FindBestWay(maze, start, finish));
        start = new Point(0, 0); 
        finish = new Point(1,1);
        Assert.IsNull(Algorithms.FindBestWay(maze, start, finish));
    }
    private static Algorithms.SinglyLinkedList<Point> GetSinglyLinkedList(List<Point> points)
    {
        if (points.Count == 0)
            return null;
        if (points.Count == 1)
            return new Algorithms.SinglyLinkedList<Point>(points[0]);
        var singlyLinkedList = new Algorithms.SinglyLinkedList<Point>(points[0]);
        for (var i = 1; i < points.Count; i++)
            singlyLinkedList = new Algorithms.SinglyLinkedList<Point>(points[i], singlyLinkedList);
        return singlyLinkedList;
    }
}
