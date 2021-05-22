using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interaction : MonoBehaviour
{
    protected bool canObjectBeInteracted;
    protected bool isPlayerReadyToInteract;
    public bool mustNotBeInteraction;
    public List<ActivateConditions> activateConditionsList;
    
    protected GameObject interactionIndicator;
    protected float invokeTime;
    
    protected void InteractionInitialize()
    {
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canObjectBeInteracted && StoryBoolActivateCondition())
        {
            GameManager.GM.ChangeStoryBool("playerCanInteractFirstTime", true);
            SetAbilities(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SetAbilities(false);
    }

    private void SetAbilities(bool canInteract)
    {
        if (!mustNotBeInteraction)
        {
            interactionIndicator.SetActive(canInteract);
            isPlayerReadyToInteract = canInteract;
        }
    }

    protected virtual bool ActivateCondition()
    {
        return Input.GetKey(GameManager.GM.KeyObgectsInteraction);
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