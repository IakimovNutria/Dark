using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonamiCode : ActivationCode
{
    public Flashlight flashlight;
    // Start is called before the first frame update
    void Start()
    {
        SetKeysSequence(new List<string>{ "up", "up", "down", "down", "left", "right", "left", "right", "b", "a"});
    }

    // Update is called once per frame
    void Update()
    {
        ActivationCodeUpdate();
    }

    protected override void Activate()
    {
        flashlight.ChangeBatteriesCount(30);
    }
}
