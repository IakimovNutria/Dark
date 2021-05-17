using System.Collections;
using System.Collections.Generic;
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
}
