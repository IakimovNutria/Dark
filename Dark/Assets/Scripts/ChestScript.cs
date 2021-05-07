using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : Interaction
{
    // Start is called before the first frame update
    public int battaries = 3;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var canvas = GameObject.FindGameObjectWithTag("Canvas");
        enterText = canvas.transform.GetChild(3).gameObject;
        light = gameObject.transform.GetChild(0).gameObject;
        canObjectBeInteracted = true;
        enterText.SetActive(false);
        light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerReadyToInteract && Input.GetKeyDown("space"))
        {
            enterText.SetActive(false);
            light.SetActive(false);
            player.GetComponent<Flashlight>().AddBattaries(battaries);
            battaries = 0;
            canObjectBeInteracted = false;
            isPlayerReadyToInteract = false;
        }
    }
}
