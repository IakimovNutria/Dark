using TMPro;
using UnityEngine;

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
