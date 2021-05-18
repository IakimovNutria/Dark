using UnityEngine;
using System.Collections;

public class Dialogue : Interaction
{
    public DialogueNode[] node;
    public int currentNode;
    protected bool isActivateDialogue;
    protected string dialogueActivateKey = "e";
    protected bool isDialogueEnd;
    public bool canRepeat;
    private bool isGameResumed;
    
    public void Start()
    {
        InteractionInitialize(4);
    }

    public void OnGUI()
    {
        if (!isActivateDialogue)
        {
            if (isDialogueEnd && !isGameResumed)
            {
                GameManager.GM.ResumeGame();
                isGameResumed = true;
            }

            isActivateDialogue = ActivateCondition() && (!isDialogueEnd || canRepeat);
            return;
        }
        
        if (!isPlayerReadyToInteract) 
        {
            isActivateDialogue = false;
            return;
        }
        
        GameManager.GM.FreezeGame();
        isGameResumed = false;
        
        GUI.Box (new Rect (Screen.width / 2 - 300, Screen.height - 200, 600, 250), "");
        GUI.Label (new Rect (Screen.width / 2 - 250, 
            Screen.height - 180, 500, 90), node [currentNode].NpcText);
        for (var i = 0; i < node [currentNode].PlayerAnswer.Length; i++)
        {
            if (!GUI.Button(new Rect(Screen.width / 2 - 250, 
                    Screen.height - 100 + 25 * i, 500, 25),
                node[currentNode].PlayerAnswer[i].Text)) continue;
            var storyBoolToChange = node[currentNode].PlayerAnswer[i].StoryBoolToChange;
            if (storyBoolToChange != "")
                GameManager.GM.ChangeStoryBool(storyBoolToChange);
            if (node [currentNode].PlayerAnswer [i].SpeakEnd) {
                if (!canRepeat)
                    canObjectBeInteracted = false;
                isDialogueEnd = true;
                isActivateDialogue = false;
            }
            currentNode = node [currentNode].PlayerAnswer [i].ToNode;
        }
        
    }

    protected override bool ActivateCondition()
    {
        return Input.GetKey(dialogueActivateKey);
    }
}


[System.Serializable]
public class DialogueNode
{
    public string NpcText;
    public Answer[] PlayerAnswer;
}


[System.Serializable]
public class Answer
{
    public string Text;
    public int ToNode;
    public bool SpeakEnd;
    public string StoryBoolToChange;
}