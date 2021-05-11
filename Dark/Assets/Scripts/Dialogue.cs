using UnityEngine;
using System.Collections;

public class Dialogue : Interaction
{
    public DialogueNode[] node;
    public int currentNode;
    private bool isActivateDialogue;
    private string dialogueActivateKey = "e";
    private void OnGUI()
    {
        if (!isActivateDialogue)
        {
            isActivateDialogue = Input.GetKey(dialogueActivateKey);
            return;
        }

        if (!isAbleToInteract)
        {
            isActivateDialogue = false;
            return;
        }
        GUI.Box (new Rect (Screen.width / 2 - 300, Screen.height - 200, 600, 250), "");
        GUI.Label (new Rect (Screen.width / 2 - 250, 
            Screen.height - 180, 500, 90), node [currentNode].NpcText);
        for (var i = 0; i < node [currentNode].PlayerAnswer.Length; i++)
        {
            if (!GUI.Button(new Rect(Screen.width / 2 - 250, 
                    Screen.height - 100 + 25 * i, 500, 25),
                node[currentNode].PlayerAnswer[i].Text)) continue;
            if (node [currentNode].PlayerAnswer [i].SpeakEnd) {
                isAbleToInteract = false;
            }
            currentNode = node [currentNode].PlayerAnswer [i].ToNode;
        }
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
}