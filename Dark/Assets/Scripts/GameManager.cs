using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool firstRoomChestVisited;
    public static GameObject flashlight;

    public static GameManager GM;
    public KeyCode KeyUp { get; set; }
    public KeyCode KeyDown { get; set; }
    public KeyCode KeyLeft { get; set; }
    public KeyCode KeyRight { get; set; }
    public KeyCode KeyDamageLight { get; set; }
    public KeyCode KeyHealLight { get; set; }
    public KeyCode KeyObgectsInteraction { get; set; }
    public KeyCode KeyDialogues { get; set; }

    public readonly Dictionary<string, bool> StoryBools = new Dictionary<string, bool>
    {
        {"isFirstRoomCleaned", false},
        {"a", false}
    };
    private void Awake()
    {
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if (GM != this)
            Destroy(gameObject);

        flashlight = GameObject.FindGameObjectWithTag("Flashlight");
        SetDefaultKeys();
    }

    private void Update()
    {
        if (StoryBools["isFirstRoomCleaned"])
        {
            
        }
        else if (!GetEnemiesInScene().Any() && !GetDiedInScene().Any())
            ChangeStoryBool("isFirstRoomCleaned");
    }
    
    public void FreezeGame()
    {
        Time.timeScale = 0;
        flashlight.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        flashlight.SetActive(true);
    }

    private void SetDefaultKeys()
    {
        KeyUp = KeyCode.W;
        KeyDown = KeyCode.S;
        KeyLeft = KeyCode.A;
        KeyRight = KeyCode.D;
        KeyDamageLight = KeyCode.RightControl;
        KeyHealLight = KeyCode.RightAlt;
        KeyObgectsInteraction = KeyCode.Space;
        KeyDialogues = KeyCode.E;
    }

    private IEnumerable<GameObject> GetEnemiesInScene()
    {
        return GameObject.FindGameObjectsWithTag("Enemy");
    }
    
    private IEnumerable<GameObject> GetDiedInScene()
    {
        return GameObject.FindGameObjectsWithTag("Died");
    }

    private void ChangeStoryBool(string storyBoolName)
    {
        StoryBools[storyBoolName] = !StoryBools[storyBoolName];
    }
}
