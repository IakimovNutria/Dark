using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject light;
    public GameObject enterText;
    public bool canObjectBeInteracted;
    public bool isPlayerReadyToInteract;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canObjectBeInteracted)
            SetAbilities(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            SetAbilities(false);
    }

    private void SetAbilities(bool canInteract)
    {
        enterText.SetActive(canInteract);
        light.SetActive(canInteract);
        isPlayerReadyToInteract = canInteract;
    }
}
