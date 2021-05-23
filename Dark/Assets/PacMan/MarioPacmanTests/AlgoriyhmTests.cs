using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.Drawing;
using UnityEngine;
using UnityEngine.TestTools;

public class AlgoriyhmTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void AlgoriyhmTestsSimplePasses()
    {
        var maze = new bool[,]
        {
            { true, true },
            { true, true }
        };
        var start = new Point(0, 0);
        var finish = new Point(0, 1);
        var expectedLength = 1;
        Assert.AreEqual(expectedLength, Algorithms.FindBestWay(maze, start, finish).Length);
    }
}
