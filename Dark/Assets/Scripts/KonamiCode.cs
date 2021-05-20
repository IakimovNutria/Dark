using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonamiCode : ActivationCode
{
    public Flashlight flashlight;
    private void Start()
    {
        SetKeysSequence(new List<string>{ "up", "up", "down", "down", "left", "right", "left", "right", "b", "a"});
    }

    private void Update()
    {
        ActivationCodeUpdate();
    }

    protected override void Activate()
    {
        flashlight.ChangeBatteriesCount(30);
    }
}
