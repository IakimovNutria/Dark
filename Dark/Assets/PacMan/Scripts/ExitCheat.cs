using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCheat : ActivationCode
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
        GameManager.GM.ResetGame();
    }

    private void OnDestroy()
    {
        GameManager.GM.ResetGame();
    }
}