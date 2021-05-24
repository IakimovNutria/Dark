using System.Collections.Generic;
using UnityEngine;

public class ActivationCode : MonoBehaviour
{
    private List<string> keysSequence;
    private int keySequencePosition;


    protected void ActivationCodeUpdate()
    {
        if (keySequencePosition == keysSequence.Count)
        {
            Activate();
            keySequencePosition = 0;
        }

        if (!Input.anyKeyDown) return;
        
        if (Input.GetKeyDown(keysSequence[keySequencePosition]))
            keySequencePosition++;
        else
            keySequencePosition = 0;
    }

    protected virtual void Activate()
    {
        
    }

    protected void SetKeysSequence(List<string> sequence)
    {
        keysSequence = sequence;
    }
}
