using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public GameObject enterText;
    public string nextScene;
    public static string prevScene;
    public static string currentScene;
    private bool isPlayerOnPortal;
    // Start is called before the first frame update
    void Start()
    {
        enterText.SetActive(false);
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOnPortal && Input.GetKeyDown("space"))
        {
            prevScene = currentScene;
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enterText.SetActive(true);
            isPlayerOnPortal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enterText.SetActive(false);
            isPlayerOnPortal = false;
        }
    }
}
