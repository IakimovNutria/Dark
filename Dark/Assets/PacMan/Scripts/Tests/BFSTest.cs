using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BFSTest
{
    [Test]
    public void BFSTestSimplePasses()
    {
        Assert.IsNull(Algorithms.FindBestWay(null, new  System.Drawing.Point(), 
            new Point()));
    }
}
