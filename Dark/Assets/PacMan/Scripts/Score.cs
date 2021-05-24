using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI scoreText;

    public void UpdateScore(int change)
    {
        if (score >= 1900)
            GameManager.GM.ContinueMainGame();
        score += change;
        scoreText.text = score.ToString();
    }
}
