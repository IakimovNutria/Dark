using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSaveManager : MonoBehaviour
{
    private List<AliveEntity> toSave = new List<AliveEntity>();
    
    [NonSerialized]
    public static string currentGameFolderPath;
    
    void Awake()
    {
        currentGameFolderPath = $"{Application.temporaryCachePath}/CurrentGame";
        if (!Directory.Exists(currentGameFolderPath))
            Directory.CreateDirectory(currentGameFolderPath);
    }

    public void SaveScene()
    {
        toSave = FindObjectsOfType<AliveEntity>()
            .Where(aliveEntity => aliveEntity.GetComponent<Player>() == null)
            .ToList();
        var root = new XElement("root");
        foreach (var obj in toSave)
            root.Add(obj.GetElement());
        var saveDoc = new XDocument(root);
        File.WriteAllText(currentGameFolderPath + $"/{SceneManager.GetActiveScene().name}.xml",
            saveDoc.ToString());
    }

    public static void DeleteSave(string saveName)
    {
        foreach (var currentGameFile in Directory.GetFiles(currentGameFolderPath))
            File.Delete(currentGameFile);
        Directory.Delete($"{Application.temporaryCachePath}/" + saveName);
    }

    /*public void SaveGame(string saveName)
    {
        var savePath = $"{Application.temporaryCachePath}" + saveName;
        Directory.CreateDirectory(savePath);

        foreach (var currentGameFile in Directory.GetFiles(currentGameFolderPath))
            File.Copy(currentGameFile, savePath, true);
    }*/
}
