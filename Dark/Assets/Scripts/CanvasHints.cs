using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHints : MonoBehaviour
{
    public Text InteractionHint;
    public Text DialogueHint;
    // Start is called before the first frame update
    void Start()
    {
        SetInteractionHint();
        SetDialogueHint();
    }

    public void SetInteractionHint()
    {
        var key = GameManager.GM.KeyObgectsInteraction.ToString();
        InteractionHint.text = $"Нажмите\n[{key}]";
    }

    public void SetDialogueHint()
    {
        var key = GameManager.GM.KeyDialogues.ToString();
        DialogueHint.text = $"Начать диалог\n[{key}]";
    }
}
