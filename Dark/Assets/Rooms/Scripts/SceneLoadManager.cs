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
    
    public int leftScene;
    public int rightScene;
    public int topScene;
    public int bottomScene;
    public Vector2 leftStart;
    public Vector2 rightStart;
    public Vector2 topStart;
    public Vector2 bottomStart;
    private SceneSaveManager sceneSaveManager;
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
    }

    private void Start()
    {
        sceneSaveManager = FindObjectOfType<SceneSaveManager>();
        var root = Load();
        if (root != null)
            SetThings(root);
    }
    public XElement Load()
    {
        var filePath = sceneSaveManager.currentGameFolderPath + $"/{SceneManager.GetActiveScene().name}.xml";
        return File.Exists(filePath) ? XDocument
                .Parse(File.ReadAllText(filePath))
                .Element("root") 
            : null;
    }

    private void SetThings(XElement root)
    {
        var allEntities = FindObjectsOfType<AliveEntity>()
            .Where(aliveEntity => aliveEntity.GetComponent<Player>() == null)
            .ToList();
        var loadedEntities = new List<AliveEntity>();
        
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

            var aliveEntity = entity.GetComponent<AliveEntity>();
            aliveEntity.SetHealth(health);
            var x = float.Parse(item.Attribute("x")?.Value!, CultureInfo.InvariantCulture);
            var y = float.Parse(item.Attribute("y")?.Value!, CultureInfo.InvariantCulture);
            entity.transform.position = new Vector2(x, y);
            loadedEntities.Add(aliveEntity);
        }

        var notLoadedEntities = allEntities.Except(loadedEntities);
        foreach (var entity in notLoadedEntities)
            entity.gameObject.SetActive(false);
    }
}
