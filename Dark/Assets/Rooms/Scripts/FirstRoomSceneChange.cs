using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRoomSceneChange : SceneChange
{
    private SceneChange sceneChange;

    private new void Start()
    {
        sceneChange = this;
        sceneChange.Start();
        canObjectBeInteracted = false;
        isPlayerReadyToInteract = true;
    }

    // Update is called once per frame
    private new void Update()
    {
        sceneChange.Update();
    }

    protected override bool ActivateCondition()
    {
        return GameManager.GM.StoryBools["isFirstDialogueEnd"];
    }
}
