using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI scoreText;

    public void UpdateScore(int change)
    {
        score += change;
        scoreText.text = score.ToString();
    }
}
