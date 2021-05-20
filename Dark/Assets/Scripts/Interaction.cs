using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool canObjectBeInteracted;
    public bool isPlayerReadyToInteract;
    public bool noInteractionIndicator;
    protected GameObject interactionIndicator;
    protected GameObject enterText;
    protected void InteractionInitialize(int textIndex)
    {
        var canvas = GameObject.FindGameObjectWithTag("Canvas");
        enterText = canvas.transform.GetChild(textIndex).gameObject;
        if (!noInteractionIndicator)
        {
            interactionIndicator = gameObject.transform.GetChild(0).gameObject;
            interactionIndicator.SetActive(false);
        }

        canObjectBeInteracted = true;
        enterText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canObjectBeInteracted)
            SetAbilities(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SetAbilities(false);
    }

    private void SetAbilities(bool canInteract)
    {
        enterText.SetActive(canInteract);
        if (!noInteractionIndicator)
            interactionIndicator.SetActive(canInteract);
        isPlayerReadyToInteract = canInteract;
    }

    protected virtual bool ActivateCondition()
    {
        throw new System.NotImplementedException();
    }
}
