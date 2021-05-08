using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energizer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D entered)
    {
        if (entered.CompareTag("Player"))
            Destroy(gameObject);
    }
}
