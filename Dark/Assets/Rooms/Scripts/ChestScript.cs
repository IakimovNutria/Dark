using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ChestScript : Interaction
{
    // Start is called before the first frame update
    public string chestName;
    public int battaries;

    private GameObject flashlight;
    private FieldInfo chestVisitedField;
    void Start()
    {
        InteractionInitialize(3);
        flashlight = GameObject.FindGameObjectWithTag("Flashlight");
        var canvas = GameObject.FindGameObjectWithTag("Canvas");
        SetBattaries();
    }

    // Update is called once per frame
    void Update()
    {
        if (battaries > 0 && Input.GetKeyDown(GameManager.GM.KeyObgectsInteraction))
        {
            enterText.SetActive(false);
            interactionIndicator.SetActive(false);
            flashlight.GetComponent<Flashlight>().ChangeBatteriesCount(battaries);
            RefuseToInteract();
            chestVisitedField.SetValue(GameManager.GM, true);
        }
    }

    private void SetBattaries()
    {
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
