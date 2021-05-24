using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource Stream;
    public AudioSource Waltz;
    private void OnLevelWasLoaded(int level)
    {
        if (level == 22)
        {
            Stream.Pause();
            Waltz.Play();
        }
    }
}
