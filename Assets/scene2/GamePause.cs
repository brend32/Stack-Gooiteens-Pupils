using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public GameObject pauseCanvas;  // Канвас для паузи
    public Button returnButton;     // Кнопка для повернення в гру
    public Button leaveButton;      // Кнопка для виходу з гри

    private bool isPaused = false;  // Статус гри (пауза чи ні)

    void Start()
    {
        // Спочатку канвас паузи прихований
        pauseCanvas.SetActive(false);

        // Додаємо слухачів подій для кнопок
        returnButton.onClick.AddListener(ReturnToGame);
        leaveButton.onClick.AddListener(LeaveGame);
    }

    void Update()
    {
        // Перевірка натискання клавіші Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ReturnToGame();
            }
        }
    }

    void PauseGame()
    {
        // Зупинити гру
        Time.timeScale = 0f;
        isPaused = true;
        pauseCanvas.SetActive(true);  // Показати канвас паузи
    }

    void ReturnToGame()
    {
        // Повернути гру в активний стан
        Time.timeScale = 1f;
        isPaused = false;
        pauseCanvas.SetActive(false); // Приховати канвас паузи
    }

    void LeaveGame()
    {
        // Вихід з гри
        Time.timeScale = 1f;  // Відновлення часу, якщо користувач вийде з гри
        Application.Quit();    // Закрити гру
    }
}