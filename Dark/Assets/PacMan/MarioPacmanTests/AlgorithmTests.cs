using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.Drawing;
using UnityEngine;
using UnityEngine.TestTools;

public class AlgorithmTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void OneStepPath()
    {
        var maze = new bool[,]
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
        var maze = new bool[,]
        {
            { true, false, true },
            { true, true, true }
        };
        var start = new Point(0, 0);
        var finish = new Point(0, 2);
        var expectedLength = 5;
        Assert.AreEqual(expectedLength, Algorithms.FindBestWay(maze, start, finish).Length);
    }

    [Test]
    public void StayIfNoPaths()
    {
        var maze = new bool[,]
        {
            { true, false, true },
            { true, false, true }
        };
        var start = new Point(0, 0);
        var finish = new Point(0, 2);
        Assert.AreEqual(null, Algorithms.FindBestWay(maze, start, finish));
    }
}
