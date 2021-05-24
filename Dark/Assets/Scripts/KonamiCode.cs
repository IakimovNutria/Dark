using System.Collections.Generic;

public class KonamiCode : ActivationCode
{
    private Flashlight flashlight;
    private void Start()
    {
        flashlight = gameObject.GetComponent<Flashlight>();
        SetKeysSequence(new List<string>{ "up", "up", "down", "down", "left", "right", "left", "right", "b", "a"});
    }

    private void Update()
    {
        ActivationCodeUpdate();
    }

    protected override void Activate()
    {
        flashlight.AddBatteries(30);
    }
}
