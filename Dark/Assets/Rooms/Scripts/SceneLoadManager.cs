using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Globalization;
using System.Xml.Linq;
using System.IO;
using System.Linq;

public class SceneLoadManager : MonoBehaviour
{
    public static int prevSceneIndex;
    private List<AliveEntity> toSave = new List<AliveEntity>();

    public int leftScene;
    public int rightScene;
    public int topScene;
    public int bottomScene;
    public Vector2 leftStart;
    public Vector2 rightStart;
    public Vector2 topStart;
    public Vector2 bottomStart;

    private string docPath;

    private void Awake()
    {
        if (prevSceneIndex == rightScene)
            Player.Instance.gameObject.transform.position = rightStart;
        if (prevSceneIndex == leftScene)
            Player.Instance.gameObject.transform.position = leftStart;
        if (prevSceneIndex == topScene)
            Player.Instance.gameObject.transform.position = topStart;
        if (prevSceneIndex == bottomScene)
            Player.Instance.gameObject.transform.position = bottomStart;
        docPath = $"{Application.temporaryCachePath}/{SceneManager.GetActiveScene().name}.xml";
    }

    private void Start()
    {
        toSave = FindObjectsOfType<AliveEntity>()
            .Where(aliveEntity => aliveEntity.GetComponent<Player>() == null)
            .ToList();
        var root = Load();
        if (root != null)
            SetThings(root);
    }

    public void Save()
    {
        var root = new XElement("root");
        foreach (var obj in toSave)
            root.Add(obj.GetElement());
        var saveDoc = new XDocument(root);
        File.WriteAllText(docPath, saveDoc.ToString());
    }

    public XElement Load()
    {
        XElement root = null;
        if (File.Exists(docPath))
            root = XDocument.Parse(File.ReadAllText(docPath)).Element("root");
        return root;
    }

    private void SetThings(XElement root)
    {
        foreach (var item in root.Elements("instance"))
        {
            var entityName = item.Attribute("name")?.Value;
            var entity = GameObject.Find(entityName);
            var health = float.Parse(item.Attribute("health")?.Value!, CultureInfo.InvariantCulture); 
            if (health == 0)
            {
                entity.SetActive(false);
                return;
            }
            entity.GetComponent<AliveEntity>().SetHealth(health);
            var x = float.Parse(item.Attribute("x")?.Value!, CultureInfo.InvariantCulture);
            var y = float.Parse(item.Attribute("y")?.Value!, CultureInfo.InvariantCulture);
            entity.transform.position = new Vector2(x, y);
        }
    }
}
