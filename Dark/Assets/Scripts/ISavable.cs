using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public interface ISavable
{
    public XElement GetElement();
}
