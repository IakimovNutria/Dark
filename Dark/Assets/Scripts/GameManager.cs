using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameObject flashlight;

    public static GameManager GM;
    public KeyCode KeyUp { get; private set; }
    public KeyCode KeyDown { get; private set; }
    public KeyCode KeyLeft { get; private set; }
    public KeyCode KeyRight { get; private set; }
    public KeyCode KeyDamageLight { get; private set; }
    public KeyCode KeyHealLight { get; private set; }
    public KeyCode KeyObjectsInteraction { get; private set; }

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
        {"isPlayerMeetStanleySecondTime", false},
        {"isPlayerAskMalcolm", false},
        {"isPlayerGiveBattery", false},
        {"isPlayerGetPaid", false}
    };
    
    private GameObject ulf;
    private GameObject malcolm;
    private GameObject firstStanley;
    private GameObject secondStanley;
    
    private bool isGMFindUlf;
    private bool isGMFindMalcolm;
    private bool isGMFindFirstStanley;
    private bool isGMFindSecondStanley;
    
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
        flashlight = GameObject.FindGameObjectWithTag("Flashlight");
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
                flashlight.GetComponent<Flashlight>().AddBatteries(20);
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

            if (SceneManager.GetActiveScene().name == "UlfAisle")
            {
                if (!isGMFindSecondStanley)
                {
                    secondStanley = GameObject.FindGameObjectWithTag("Stanley");
                    secondStanley.SetActive(!StoryBools["isPlayerMeetStanleySecondTime"] && 
                                            (StoryBools["isPlayerMeetUlf"] || StoryBools["isPlayerTakeBattery"]));
                    isGMFindSecondStanley = true;
                }

                StoryBools["isPlayerMeetStanleySecondTime"] = StoryBools["isPlayerMeetUlf"] || 
                                                              StoryBools["isPlayerTakeBattery"];
            }
            else
                isGMFindSecondStanley = false;
            
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
        flashlight.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        flashlight.SetActive(true);
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
        KeyObjectsInteraction = KeyCode.Space;
    }

    private static IEnumerable<GameObject> GetEnemiesInScene()
    {
        return GameObject.FindGameObjectsWithTag("Enemy");
    }
    
    private static IEnumerable<GameObject> GetDiedInScene()
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
