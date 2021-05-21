using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool firstRoomChestVisited;
    public static GameObject Flashlight;

    public static GameManager GM;
    public KeyCode KeyUp { get; set; }
    public KeyCode KeyDown { get; set; }
    public KeyCode KeyLeft { get; set; }
    public KeyCode KeyRight { get; set; }
    public KeyCode KeyDamageLight { get; set; }
    public KeyCode KeyHealLight { get; set; }
    public KeyCode KeyObgectsInteraction { get; set; }
    public KeyCode KeyDialogues { get; set; }

    public bool isGameFreezed;

    public readonly Dictionary<string, bool> StoryBools = new Dictionary<string, bool>
    {
        {"isGameStarted", false},
        {"isFirstRoomCleaned", false},
        {"isFirstDialogueEnd", false},
        {"isPlayerHelpEli", false},
        {"playerCanInteractFirstTime", false},
        {"isEliReachedRoom", false},
        {"isFirstEmilyAndEliDialogueEnd", false},
        {"isPlayerHelpEmily", false},
        {"isPlayerAskedAboutToy", false},
        {"isPlayerAskedAboutLeg", false},
        {"isPlayerGetToy", false},
        {"isPlayerGiveToy", false},
        {"drawHorse", false},
        {"drawFlower", false},
        {"drawMarioPacman", false},
        {"draw", false},
        {"doesPlayerKnowStanley", false},
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

        InitGameManager();
    }

    private void InitGameManager()
    {
        Flashlight = GameObject.FindGameObjectWithTag("Flashlight");
        SetDefaultKeys();
    }

    private void Update()
    {
        if (StoryBools["isFirstRoomCleaned"])
        {
            if (StoryBools["isPlayerGiveToy"])
                StoryBools["draw"] = StoryBools["drawFlower"] || StoryBools["drawHorse"] 
                                                              || StoryBools["drawMarioPacman"];
        }
        else if (!GetEnemiesInScene().Any() && !GetDiedInScene().Any() && StoryBools["isGameStarted"])
            ChangeStoryBool("isFirstRoomCleaned", true);
        else if (GetEnemiesInScene().Any() && !StoryBools["isGameStarted"])
            ChangeStoryBool("isGameStarted", true);
    }
    
    public void FreezeGame()
    {
        isGameFreezed = true;
        Flashlight.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Flashlight.SetActive(true);
        isGameFreezed = false;
    }

    private void SetDefaultKeys()
    {
        KeyUp = KeyCode.W;
        KeyDown = KeyCode.S;
        KeyLeft = KeyCode.A;
        KeyRight = KeyCode.D;
        KeyDamageLight = KeyCode.C;
        KeyHealLight = KeyCode.V;
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

    public void ChangeStoryBool(string storyBoolName, bool storyBoolValue)
    {
        StoryBools[storyBoolName] = storyBoolValue;
    }
}
