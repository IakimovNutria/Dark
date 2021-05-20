using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoad : MonoBehaviour
{
    public static int prevSceneIndex;

    public int leftScene;
    public int rightScene;
    public int topScene;
    public int bottomScene;
    public Vector2 leftStart;
    public Vector2 rightStart;
    public Vector2 topStart;
    public Vector2 bottomStart;

    private void OnLevelWasLoaded(int level)
    {
        if (prevSceneIndex == rightScene)
            Player.Instance.gameObject.transform.position = rightStart;
        if (prevSceneIndex == leftScene)
            Player.Instance.gameObject.transform.position = leftStart;
        if (prevSceneIndex == topScene)
            Player.Instance.gameObject.transform.position = topStart;
        if (prevSceneIndex == bottomScene)
            Player.Instance.gameObject.transform.position = bottomStart;
    }
}
