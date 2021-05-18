using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEliDialogue : Dialogue
{
    private void Start()
    {
        DialogueStart();
        canObjectBeInteracted = false;
        isPlayerReadyToInteract = true;
    }

    protected override bool ActivateDialogueCondition()
    {
        return GameManager.GM.StoryBools["isFirstRoomCleaned"];
    }

    private void OnGUI()
    {
        DialogueOnGUI();
    }
}
