using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;      // Текст для відображення рахунку
    private int score = 0;      // Поточний рахунок
    private int clickCount = 0; // Лічильник кліків

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        // Перевірка на кліки лівою кнопкою миші
        if (Input.GetMouseButtonDown(0)) // ЛКМ (кнопка 0 - ліва кнопка)
        {
            clickCount++;

            // Якщо кількість кліків більше або дорівнює 3, змінюємо колір тексту на червоний
            if (clickCount >= 3)
            {
                scoreText.color = Color.red;
            }

            // Збільшуємо рахунок на 1
            score++;

            // Оновлюємо текст рахунку
            UpdateScoreText();
        }
    }

    // Оновлення тексту рахунку на UI
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString("D5");
    }
}