using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    void Start()
    {
        score = 0;
        UpdateText();
    }

    public void AddScore(int sum)
    {
        score += sum;
        UpdateText();
    }

    public void UpdateText()
    {
        scoreText.text = $"Score: {score}";
    }
}