using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoorDialogue : Dialogue
{
    private Dialogue dialogue;

    private new void Start()
    {
        dialogue = gameObject.GetComponent<ClosedDoorDialogue>();
        dialogue.Start();
        canRepeat = true;
    }

    private new void OnGUI()
    {
        dialogue.OnGUI();
    }
}
