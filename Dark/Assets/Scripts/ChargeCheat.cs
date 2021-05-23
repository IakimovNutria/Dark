using System.Collections.Generic;

public class ChargeCheat : ActivationCode
{
    public Flashlight flashLight;

    private bool isActive;
    
    void Start()
    {
        SetKeysSequence(new List<string>{"c","h","e","a","t"});
    }

    // Update is called once per frame
    void Update()
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
