using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public GameObject enterText;
    public string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        enterText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enterText.SetActive(true);
            if (Input.GetKeyDown("space"))
                SceneManager.LoadScene(nextScene);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            enterText.SetActive(false);
    }
}
