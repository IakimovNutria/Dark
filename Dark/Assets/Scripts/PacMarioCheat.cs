using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMarioCheat : ActivationCode
{
    private void Start()
    {
        SetKeysSequence(new List<string>{"m", "a", "r", "i", "o"});
    }

    private void Update()
    {
        ActivationCodeUpdate();
    }

    protected override void Activate()
    {
        GameManager.GM.PlayMarioPacman();
    }
}
