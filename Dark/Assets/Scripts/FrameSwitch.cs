using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameSwitch : MonoBehaviour
{
    private GameObject activeFrame;
    // Start is called before the first frame update
    void Start()
    {
        activeFrame = gameObject.transform.GetChild(0).gameObject;
        if (gameObject.tag != "Start")
            activeFrame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activeFrame.activeSelf)
            activeFrame.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player") && activeFrame.activeSelf)
            activeFrame.SetActive(false);
    }
}
