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
    private List<ISavable> entitiesToSave = new List<ISavable>();
    
    private static readonly List<string> notToSaveScenes = new List<string>
    {
        "UlfRoom", "Aisle"
    };
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
        if (notToSaveScenes.Any(scene => SceneManager.GetActiveScene().name == scene))
            return;
        
        entitiesToSave = FindObjectsOfType<AliveEntity>()
            .Where(aliveEntity => aliveEntity.GetComponent<Player>() == null)
            .Select(aliveEntity => (ISavable)aliveEntity)
            .Union(FindObjectsOfType<Chest>()
                .Select(chest => (ISavable)chest))
            .ToList();
        
        var root = new XElement("root");
        
        foreach (var entity in entitiesToSave)
            root.Add(entity.GetElement());
        
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
