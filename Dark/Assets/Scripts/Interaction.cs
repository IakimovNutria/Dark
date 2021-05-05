using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    //public GameObject enterText;
    public bool isAbleToInteract;
    // Start is called before the first frame update
    void Start()
    {
        //enterText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //enterText.SetActive(true);
            isAbleToInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //enterText.SetActive(false);
            isAbleToInteract = false;
        }
    }
}
