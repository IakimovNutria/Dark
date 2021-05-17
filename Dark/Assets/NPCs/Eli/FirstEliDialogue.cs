using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEliDialogue : Dialogue
{
    void Update()
    {
        isPlayerReadyToInteract = true;
        if (isActivateDialogue) return;
        isActivateDialogue = GameManager.StoryBools["isFirstRoomCleaned"];
    }
}
