using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

public class SavableObject : MonoBehaviour
{
    private SceneLoad loader;
    void Awake()
    {
        loader = FindObjectOfType<SceneLoad>();
        loader.toSave.Add(this);
    }

    public XElement GetElement()
    {
        var objName = new XAttribute("name", name);
        var x = new XAttribute("x", transform.position.x);
        var y = new XAttribute("y", transform.position.y);
        var enemy = gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            var health = new XAttribute("health", enemy.Health);
            return new XElement("instance", objName, health, x, y);
        }
        else
            return new XElement("instance", objName, x, y);
    }
}
