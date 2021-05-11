using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeCheat : ActivationCode
{
    public Flashlight flashLight;

    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        SetKeysSequence(new List<string>{"c","h","e","a","t"});
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
            flashLight.ChangeCharge(1000);
        ActivationCodeUpdate();
    }

    protected override void Activate()
    {
        isActive = !isActive;
    }
}
