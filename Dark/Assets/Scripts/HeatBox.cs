using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatBox : MonoBehaviour
{
    private float damage;
    private void Start()
    {
        Light2DEmitter.OnBeamStay += OnBeamStay;
        Light2DEmitter.OnBeamEnter += OnBeamEnter;
        Light2DEmitter.OnBeamExit += OnBeamExit;
    }
    
    private void OnBeamEnter(GameObject obj, Light2DEmitter emitter)
    {
        
    }
    private void OnBeamStay(GameObject obj, Light2DEmitter emitter)
    {
        if (obj.GetInstanceID() == gameObject.GetInstanceID() && emitter.eventPassedFilter == "DamageEnemy")
        {
            damage += 0.1f;
        }
    }
    private void OnBeamExit(GameObject obj, Light2DEmitter emitter)
    {
        
    }

    public float GetDamage()
    {
        var takenDamage = damage;
        damage = 0;
        return takenDamage;
    }
}
