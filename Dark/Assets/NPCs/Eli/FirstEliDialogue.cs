using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEliDialogue : Dialogue
{
    private new void Start()
    {
        dialogue = this;
        dialogue.Start();
        canObjectBeInteracted = false;
        isPlayerReadyToInteract = true;
    }

    protected override bool ActivateCondition()
    {
        return GameManager.GM.StoryBools["isFirstRoomCleaned"];
    }

    private new void OnGUI()
    {
        dialogue.OnGUI();
    }
}
