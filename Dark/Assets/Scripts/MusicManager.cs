using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource Stream;
    public AudioSource Waltz;
    public AudioSource PacMario;
    private void OnLevelWasLoaded(int level)
    {
        if (level == 22)
        {
            Stream.Pause();
            Waltz.Play();
        }
        else if (level == 21)
        {
            Stream.Pause();
            PacMario.Play();
        }
        else if (level == 16 && GameManager.GM.StoryBools["isPlayerBelieveInMarioPacman"] && GameManager.GM.StoryBools["PlayerAgreeToPlayMarioPacman"])
        {
            PacMario.Pause();
            Stream.Play();
        }
    }
}
