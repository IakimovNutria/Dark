using System.Collections.Generic;
using UnityEngine;

public class Training : Interaction
{
    private Player player;
    public Flashlight flashlight;

    private static readonly List<string> MainTrainingText = new List<string>
    {
        "Для передвижения используйте кнопки " + GameManager.GM.KeyUp + " " +
                                         GameManager.GM.KeyLeft + " " +
                                         GameManager.GM.KeyDown + " " +
                                         GameManager.GM.KeyRight,
        "Для того, чтобы включить фонарик нажмите " + GameManager.GM.KeyDamageLight, 
        "Чтобы поставить игру на паузу нажмите esc"
    };

    private static readonly List<string> HealTrainingText = new List<string>
    {
        "Когда тьма подходит к вам слишком близко, вы получаете урон",
        "Для того, чтобы направить фонарик на себя и согнать тьму нажмите " + GameManager.GM.KeyHealLight
    };

    private static readonly List<string> ChargeTrainingText = new List<string>
    {
        "Когда фонарик включен, его заряд падает",
        "Чтобы выключить фонарик нажмите кнопку " + GameManager.GM.KeyDamageLight
    };
    
    private static readonly List<string> BatteriesTrainingText = new List<string>
    {
        "Когда заряд фонарика падает до критической отметки, вы используете батарейку",
        "Чтобы выжить вам нужны батарейки, ищите их"
    };

    private static readonly List<string> InteractionTrainingText = new List<string>
    {
        "Чтобы взаимодействовать нажмите кнопку " + GameManager.GM.KeyObjectsInteraction
    };
    
    private int currentMainTrainingTextIndex; 
    private int currentHealTrainingTextIndex; 
    private int currentChargeTrainingTextIndex; 
    private int currentBatteriesTrainingTextIndex;
    private int currentInteractionTrainingTextIndex;

    private bool isMainTrainingGameContinued;
    private bool isHealTrainingGameContinued;
    private bool isChargeTrainingGameContinued;
    private bool isBatteriesTrainingGameContinued;
    private bool isInteractionTrainingGameContinued;
    
    private void Start()
    {
        player = gameObject.GetComponent<Player>();
        canObjectBeInteracted = false;
        isPlayerReadyToInteract = true;
    }

    protected override bool ActivateCondition()
    {
        return GameManager.GM.StoryBools["isGameStarted"];
    }


    private void OnGUI()
    {
        if (currentMainTrainingTextIndex != MainTrainingText.Count)
            OnGuiTraining(MainTrainingText, ref currentMainTrainingTextIndex);
        else if (!isMainTrainingGameContinued)
        {
            isMainTrainingGameContinued = true;
            GameManager.GM.ResumeGame();
        }

        if (currentHealTrainingTextIndex != HealTrainingText.Count)
        {
            if (player.Health < player.MAXPlayerHealth * 0.8f)
                OnGuiTraining(HealTrainingText, ref currentHealTrainingTextIndex);
        }
        else if (!isHealTrainingGameContinued)
        {
            isHealTrainingGameContinued = true;
            GameManager.GM.ResumeGame();
        }

        if (currentChargeTrainingTextIndex != ChargeTrainingText.Count)
        {
            if (flashlight.Charge < flashlight.MAXFlashlightCharge * 0.8f)
                OnGuiTraining(ChargeTrainingText, ref currentChargeTrainingTextIndex);
        }
        else if (!isChargeTrainingGameContinued)
        {
            isChargeTrainingGameContinued = true;
            GameManager.GM.ResumeGame();
        }

        if (currentBatteriesTrainingTextIndex != BatteriesTrainingText.Count)
        {
            if (flashlight.BatteriesCount == flashlight.StartBatteriesCount - 1)
                OnGuiTraining(BatteriesTrainingText, ref currentBatteriesTrainingTextIndex);
        }
        else if (!isBatteriesTrainingGameContinued)
        {
            isBatteriesTrainingGameContinued = true;
            GameManager.GM.ResumeGame();
        }

        if (currentInteractionTrainingTextIndex != InteractionTrainingText.Count)
        {
            if (GameManager.GM.StoryBools["playerCanInteractFirstTime"])
                OnGuiTraining(InteractionTrainingText, ref currentInteractionTrainingTextIndex);
        }
        else if (!isInteractionTrainingGameContinued)
        {
            isInteractionTrainingGameContinued = true;
            GameManager.GM.ResumeGame();
        }
    }

    private static void OnGuiTraining(IReadOnlyList<string> trainingText, ref int index)
    {
        GameManager.GM.FreezeGame();
        GUI.Box (new Rect (Screen.width / 2 - 300, Screen.height - 250, 600, 240), 
            "Обучение");
        GUI.Label (new Rect (Screen.width / 2 - 250, 
            Screen.height - 180, 500, 90), trainingText[index]);
        if (GUI.Button(new Rect(Screen.width / 2 - 250, 
            Screen.height - 100 + 25, 500, 25), 
            "Далее"))
            index++;
    }
}
