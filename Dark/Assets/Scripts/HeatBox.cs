using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatBox : MonoBehaviour
{
    private float damage;
    public GameObject enemyGameObject;
    private Enemy enemy;
    //private SpriteRenderer enemySpriteRenderer;
    private void Start()
    {
        enemy = enemyGameObject.GetComponent<Enemy>();
        //enemySpriteRenderer = enemyGameObject.GetComponent<SpriteRenderer>();
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
            enemy.TakeDamage(0.1f);
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
