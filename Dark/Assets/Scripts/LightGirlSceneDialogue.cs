using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGirlSceneDialogue : Dialogue
{
    private Dialogue dialogue;
    private new void Start()
    {
        dialogue = this;
        dialogue.Start();
        canObjectBeInteracted = false;
        isPlayerReadyToInteract = true;
    }

    protected override bool ActivateCondition()
    {
        return true;
    }
    
    private new void OnGUI()
    {
        dialogue.OnGUI();
    }
}
