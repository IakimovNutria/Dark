using System;
using UnityEngine;
using System.Collections;

public class Dialogue : Interaction
{
    public DialogueNode[] node;
    public int currentNode;
    protected bool isActivateDialogue;
    protected string dialogueActivateKey = "e";
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

            isActivateDialogue = ActivateCondition() && (!isDialogueEnd || canRepeat);
            return;
        }
        
        if (!isPlayerReadyToInteract) 
        {
            isActivateDialogue = false;
            return;
        }
        if (node[currentNode].storyBoolToGetDialogue != "")
            if (!GameManager.GM.StoryBools[node[currentNode].storyBoolToGetDialogue])
            {
                currentNode = node[currentNode].toNode;
                return;
            }
        GameManager.GM.FreezeGame();
        isGameResumed = false;
        GUI.Box (new Rect (Screen.width / 2 - 300, Screen.height - 200, 600, 250), "");
        GUI.Label (new Rect (Screen.width / 2 - 250, 
            Screen.height - 180, 500, 90), node[currentNode].npcText);
        
        for (var i = 0; i < node[currentNode].playerAnswer.Length; i++)
        {
            if (node[currentNode].playerAnswer[i].IsAnswered && 
                !node[currentNode].playerAnswer[i].canRepeat)
                continue;
            
            if (!GUI.Button(new Rect(Screen.width / 2 - 250, 
                    Screen.height - 100 + 25 * i, 500, 25),
                node[currentNode].playerAnswer[i].text)) continue;
            node[currentNode].playerAnswer[i].IsAnswered = true;
            var storyBoolToChange = node[currentNode].playerAnswer[i].storyBoolToChange;
            if (storyBoolToChange != "")
                GameManager.GM.ChangeStoryBool(storyBoolToChange);
            if (node [currentNode].playerAnswer [i].speakEnd) {
                if (!canRepeat)
                    canObjectBeInteracted = false;
                isDialogueEnd = true;
                isActivateDialogue = false;
            }
            currentNode = node [currentNode].playerAnswer [i].toNode;
        }
        
    }

    protected override bool ActivateCondition()
    {
        return Input.GetKey(dialogueActivateKey);
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
    
    public bool canRepeat;
    public string text;
    public int toNode;
    public bool speakEnd;
    public string storyBoolToChange;
}