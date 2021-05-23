using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        {"exitGame", false}, 
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
        {"isPlayerMeetStanleyFirstTime", false},
        {"isPlayerKnowMalcolm", false},
        {"isPlayerHelpMalcolm", false},
        {"isPlayerMeetUlf", false},
        {"isPlayerKnowUlf", false},
        {"isPlayerTakeBattery", false},
        {"UlfKnowWhereMalcolm", false},
        {"isPlayerBelieveInMarioPacman", false},
        {"isPlayerKnowUlfStory", false},
        {"isPlayerAskMalcolm", false},
        {"isPlayerGiveBattery", false},
        {"isPlayerGetPaid", false}
    };
    
    private GameObject ulf;
    private GameObject malcolm;
    private GameObject firstStanley;
    
    private bool isGMFindUlf;
    private bool isGMFindMalcolm;
    private bool isGMFindFirstStanley;
    
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

            if (StoryBools["isPlayerGiveBattery"] && !StoryBools["isPlayerGetPaid"])
            {
                ChangeStoryBool("isPlayerGetPaid", true);
                Flashlight.GetComponent<Flashlight>().AddBatteries(20);
            }

            if (SceneManager.GetActiveScene().name == "Aisle")
            {
                if (!isGMFindFirstStanley)
                {
                    firstStanley = GameObject.FindGameObjectWithTag("Stanley");
                    firstStanley.SetActive(!StoryBools["isPlayerMeetStanleyFirstTime"]);
                    isGMFindFirstStanley = true;
                }

                StoryBools["isPlayerMeetStanleyFirstTime"] = true;
            }
            else
                isGMFindFirstStanley = false;
            if (SceneManager.GetActiveScene().name == "UlfRoom")
            {
                if (!isGMFindUlf)
                {
                    ulf = GameObject.FindGameObjectWithTag("UlfWalk");
                    if ((StoryBools["isPlayerMeetUlf"] || StoryBools["isPlayerTakeBattery"])
                        && !StoryBools["UlfKnowWhereMalcolm"])
                        ulf.transform.position = new Vector3(0.36f, 1.14f);
                    isGMFindUlf = true;
                }
                if (!StoryBools["isPlayerHelpMalcolm"])
                    ulf.transform.position = new Vector3(0.36f, 1.14f);
                else
                    ulf.SetActive((StoryBools["isPlayerMeetUlf"] || StoryBools["isPlayerTakeBattery"]) 
                                      && !StoryBools["UlfKnowWhereMalcolm"]);
            }
            else
                isGMFindUlf = false;

            if (SceneManager.GetActiveScene().name == "MalcolmRoom")
            {
                if (!isGMFindMalcolm)
                {
                    malcolm = GameObject.FindGameObjectWithTag("Malcolm");
                    isGMFindMalcolm = true;
                }
                if (StoryBools["UlfKnowWhereMalcolm"])
                    malcolm.SetActive(false);
            }
            else
                isGMFindMalcolm = false;
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

    public void ResetGame()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("MainCamera"));
        Destroy(GameObject.FindGameObjectWithTag("Flashlight"));
        Destroy(GameObject.FindGameObjectWithTag("Canvas"));
        SceneSaveManager.DeleteSave("CurrentGame");
        SceneManager.LoadScene("GameStartMenu");
        Destroy(gameObject);
    }
}
