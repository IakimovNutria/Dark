using System.Collections.Generic;
using UnityEngine;

public class HealthCheat : ActivationCode
{
    private Player player;

    private bool isActive;

    private void Start()
    {
        player = gameObject.GetComponent<Player>();
        SetKeysSequence(new List<string>{"c","h","e","a","t"});
    }

    private void Update()
    {
        if (isActive)
            player.ChangeHealthAmount(1000);
        ActivationCodeUpdate();
    }

    protected override void Activate()
    {
        isActive = !isActive;
    }
}
