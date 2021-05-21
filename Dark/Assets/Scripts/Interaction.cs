using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interaction : MonoBehaviour
{
    public bool canObjectBeInteracted;
    public bool isPlayerReadyToInteract;
    public bool mustNotBeInteraction;
    public List<ActivateConditions> activateConditionsList;
    
    protected GameObject interactionIndicator;
    protected GameObject enterText;
    protected float invokeTime;
    
    protected void InteractionInitialize(int textIndex)
    {
        var canvas = GameObject.FindGameObjectWithTag("Canvas");
        enterText = canvas.transform.GetChild(textIndex).gameObject;
        if (mustNotBeInteraction)
        {
            canObjectBeInteracted = false;
            isPlayerReadyToInteract = true;
            invokeTime = 1;
        }
        else
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
        if (!mustNotBeInteraction)
        {
            interactionIndicator.SetActive(canInteract);
            isPlayerReadyToInteract = canInteract;
        }
    }

    protected virtual bool ActivateCondition()
    {
        throw new NotImplementedException();
    }

    protected bool StoryBoolActivateCondition()
    {
        var doesOneConditionActivated = activateConditionsList
            .Select(activateConditions => activateConditions.activateConditions
                .Where(activateCondition => !string.IsNullOrEmpty(activateCondition.storyBoolToActivate))
                .All(activateCondition => 
                    GameManager.GM.StoryBools[activateCondition.storyBoolToActivate] != activateCondition.mustBeFalse))
            .Any(doesConditionActivated => doesConditionActivated);

        return doesOneConditionActivated || activateConditionsList.Count == 0;
    }
}

[Serializable]
public class ActivateConditions
{
    public List<ActivateCondition> activateConditions;
}

[Serializable]
public class ActivateCondition
{
    public string storyBoolToActivate;
    public bool mustBeFalse;
}