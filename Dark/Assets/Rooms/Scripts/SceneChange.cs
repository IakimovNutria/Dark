using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : Interaction
{
    public string nextScene;
    public List<ActivateConditions> activateConditionsList;
    public bool mustNotBeInteraction;
    
    public void Start()
    {
        if (mustNotBeInteraction)
        {
            canObjectBeInteracted = false;
            isPlayerReadyToInteract = true;
        }
        InteractionInitialize(3);
    }

    public void Update()
    {
        var doesOneConditionActivated = activateConditionsList
            .Select(activateConditions => activateConditions.activateConditions
                .Where(activateCondition => !string.IsNullOrEmpty(activateCondition.storyBoolToActivate))
                .All(activateCondition => 
                    GameManager.GM.StoryBools[activateCondition.storyBoolToActivate] != activateCondition.mustBeFalse))
            .Any(doesConditionActivated => doesConditionActivated);

        if (!doesOneConditionActivated && activateConditionsList.Count != 0)
            return;


        if (isPlayerReadyToInteract && (ActivateCondition() || mustNotBeInteraction) && nextScene != "")
            ChangeScene();
    }

    private void ChangeScene()
    {
        SceneLoad.prevSceneIndex = SceneManager.GetActiveScene().buildIndex;
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Flashlight"));
        SceneManager.LoadScene(nextScene);
    }

    protected override bool ActivateCondition()
    {
        return Input.GetKeyDown(GameManager.GM.KeyObgectsInteraction);
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
