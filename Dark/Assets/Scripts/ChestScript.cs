using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ChestScript : Interaction
{
    // Start is called before the first frame update
    public string chestName;
    public int battaries;

    private GameObject player;
    private SceneController sceneController;
    private FieldInfo chestVisitedField;
    void Start()
    {
        InteractionInitialize(3);
        player = GameObject.FindGameObjectWithTag("Player");
        SetBattaries();
    }

    // Update is called once per frame
    void Update()
    {
        if (battaries > 0 && Input.GetKeyDown("space"))
        {
            enterText.SetActive(false);
            interactionIndicator.SetActive(false);
            player.GetComponent<Flashlight>().ChangeBatteriesCount(battaries);
            RefuseToInteract();
            chestVisitedField.SetValue(sceneController, true);
        }
    }

    private void SetBattaries()
    {
        sceneController = new SceneController();
        chestVisitedField = typeof(SceneController).GetField(chestName + "ChestVisited");
        if ((bool)chestVisitedField.GetValue(chestVisitedField))
            RefuseToInteract();
    }

    private void RefuseToInteract()
    {
        canObjectBeInteracted = false;
        battaries = 0;
    }
}
