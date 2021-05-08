using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Score score;

    private void Start()
    {
        score = FindObjectOfType<Score>();
    }

    private void OnTriggerEnter2D(Collider2D entered)
    {
        if (entered.CompareTag("Player"))
        {
            score.UpdateScore(10);
            Destroy(gameObject);
        }
    }
}
