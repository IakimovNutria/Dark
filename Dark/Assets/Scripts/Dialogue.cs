using System;
using UnityEngine;
using System.Collections;

public class Dialogue : Interaction
{
    public DialogueNode[] node;
    public int currentNode;
    protected bool isActivateDialogue;
    protected bool isDialogueEnd;
    private bool isGameResumed;
    public bool canRepeat;
    protected Dialogue dialogue;
    
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

            isActivateDialogue = ActivateCondition() && (!isDialogueEnd || canRepeat) && StoryBoolActivateCondition();
            return;
        }
        
        if (!isPlayerReadyToInteract) 
        {
            isActivateDialogue = false;
            return;
        }
        if (!string.IsNullOrEmpty(node[currentNode].storyBoolToGetDialogue))
            if (!GameManager.GM.StoryBools[node[currentNode].storyBoolToGetDialogue])
            {
                currentNode = node[currentNode].toNode;
                return;
            }
        
        GameManager.GM.FreezeGame();
        isGameResumed = false;
        
        OnGuiDialogue();
    }

    private void OnGuiDialogue()
    {
        GUI.Box (new Rect (Screen.width / 2 - 300, Screen.height - 250, 600, 240), "");
        GUI.Label (new Rect (Screen.width / 2 - 250, 
            Screen.height - 230, 500, 90), node[currentNode].npcText);


        var indention = node[currentNode].npcText == "" ? -260 : -160;

        var buttonCount = 0;
        foreach (var answer in node[currentNode].playerAnswer)
        {
            buttonCount++;
            if (answer.IsAnswered &&
                answer.cantRepeat || 
                !string.IsNullOrEmpty(answer.storyBoolToGetAnswer) &&
                GameManager.GM.StoryBools[answer.storyBoolToGetAnswer] == answer.storyBoolMustBeFalse)
            {
                buttonCount--;
                continue;
            }

            if (!GUI.Button(new Rect(Screen.width / 2 - 250, 
                    Screen.height + indention + 30 * buttonCount, 500, 25),
                string.IsNullOrEmpty(answer.text) ? "..." : answer.text)) continue;
            answer.IsAnswered = true;
            var storyBoolToChange = answer.storyBoolToChange;
            if (!string.IsNullOrEmpty(storyBoolToChange))
                GameManager.GM.ChangeStoryBool(storyBoolToChange, !answer.changeStoryBoolToFalse);
            if (answer.speakEnd) {
                if (!canRepeat)
                    canObjectBeInteracted = false;
                isDialogueEnd = true;
                isActivateDialogue = false;
            }
            currentNode = answer.toNode;
        }
    }
}


[Serializable]
public class DialogueNode
{
    public string storyBoolToGetDialogue;
    public int toNode;
    public string npcText;
    public Answer[] playerAnswer;
}


[Serializable]
public class Answer
{
    [NonSerialized]
    public bool IsAnswered;
    
    public bool cantRepeat;
    public string text;
    public int toNode;
    public bool speakEnd;
    public string storyBoolToChange;
    public string storyBoolToGetAnswer;
    public bool storyBoolMustBeFalse;
    public bool changeStoryBoolToFalse;
}