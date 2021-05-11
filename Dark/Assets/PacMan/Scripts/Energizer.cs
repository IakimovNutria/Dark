using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energizer : MonoBehaviour
{
    private Mario mario;

    private void Start()
    {
        mario = (Mario)FindObjectOfType(typeof(Mario));
    }

    private void OnTriggerEnter2D(Collider2D entered)
    {
        if (entered.CompareTag("Player"))
        {
            mario.Energize();
            Destroy(gameObject);
        }
    }
}
