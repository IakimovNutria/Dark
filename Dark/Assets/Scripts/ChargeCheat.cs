using System.Collections.Generic;
using UnityEngine;

public class ChargeCheat : ActivationCode
{
    private Flashlight flashLight;

    private bool isActive;

    private void Start()
    {
        flashLight = gameObject.GetComponent<Flashlight>();
        SetKeysSequence(new List<string>{"c","h","e","a","t"});
    }

    private void Update()
    {
        if (isActive)
            flashLight.AddCharge(1000);
        ActivationCodeUpdate();
    }

    protected override void Activate()
    {
        isActive = !isActive;
    }
}
