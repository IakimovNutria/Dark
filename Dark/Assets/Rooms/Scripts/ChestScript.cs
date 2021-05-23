using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ChestScript : Interaction
{
    public string chestName;
    public int battaries;

    private FieldInfo chestVisitedField;
    private Flashlight flashlight;

    void Start()
    {
        flashlight = FindObjectOfType<Flashlight>();
        InteractionInitialize();
        SetBattaries();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerReadyToInteract && canObjectBeInteracted && Input.GetKeyDown(GameManager.GM.KeyObgectsInteraction))
        {
            interactionIndicator.SetActive(false);
            flashlight.AddBatteries(battaries);
            RefuseToInteract();
            chestVisitedField.SetValue(GameManager.GM, true);
        }
    }

    private void SetBattaries()
    {
        GameManager.GM.ChestSound.Play();
        chestVisitedField = GameManager.GM.GetType().GetField(chestName + "ChestVisited");
        if ((bool)chestVisitedField.GetValue(chestVisitedField))
            RefuseToInteract();
    }

    private void RefuseToInteract()
    {
        canObjectBeInteracted = false;
        battaries = 0;
    }
}
