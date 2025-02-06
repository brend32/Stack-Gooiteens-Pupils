using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;  // Додаємо посилання на TextMeshPro
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button settingsButton;
    public Button exitButton;
    public Button backButton;  // Кнопка "Назад"
    public GameObject settingsPanel;
    public Button res1600x900;
    public Button res1260x720;
    public Button res900x480;
    public Button res640x420;
    public Toggle fullscreenToggle;  // Тогл для fullscreen
    public TMP_Text messageText;  // Замість Text використовуємо TMP_Text

    void Start()
    {
        settingsPanel.SetActive(false);
        StartCoroutine(AnimateButtons());

        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        exitButton.onClick.AddListener(ExitGame);
        backButton.onClick.AddListener(CloseSettings);

        res1600x900.onClick.AddListener(() => SetResolution(1600, 900));
        res1260x720.onClick.AddListener(() => SetResolution(1260, 720));
        res900x480.onClick.AddListener(() => SetResolution(900, 480));
        res640x420.onClick.AddListener(() => SetResolution(640, 420));

        fullscreenToggle.onValueChanged.AddListener(ToggleFullscreen);

        // Початкове повідомлення
        UpdateMessage("Game about the cubes");
    }

    IEnumerator AnimateButtons()
    {
        float duration = 0.5f;
        Button[] buttons = { startButton, settingsButton, exitButton, backButton };
        foreach (Button btn in buttons)
        {
            btn.gameObject.SetActive(false);
        }

        messageText.maxVisibleCharacters = 0;
        yield return new WaitForSeconds(0.5f);

        float textAnimationStart = Time.time;
        float textDuration = 0.5f;
        while (textAnimationStart + textDuration > Time.time)
        {
            float t = (Time.time - textAnimationStart) / textDuration;
            messageText.maxVisibleCharacters = Mathf.CeilToInt(Mathf.Lerp(0, messageText.text.Length, t));
            yield return null;
        }
        
        messageText.maxVisibleCharacters = messageText.text.Length;

        foreach (Button btn in buttons)
        {
            btn.gameObject.SetActive(true);
            CanvasGroup cg = btn.GetComponent<CanvasGroup>();
            if (cg == null)
                cg = btn.gameObject.AddComponent<CanvasGroup>();
            
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                cg.alpha = Mathf.Lerp(0, 1, elapsed / duration);
                yield return null;
            }
            cg.alpha = 1;
        }
    }

    void StartGame()
    {
        UpdateMessage("Завантаження гри...");
        SceneManager.LoadScene("Game"); // Замініть на вашу сцену гри
    }

    void OpenSettings()
    {
        settingsPanel.SetActive(true);
        startButton.gameObject.SetActive(false);  // Сховати кнопку Start
        settingsButton.gameObject.SetActive(false); // Сховати кнопку Settings
        exitButton.gameObject.SetActive(false);    // Сховати кнопку Exit
        UpdateMessage("Налаштування...");
    }

    void CloseSettings()
    {
        settingsPanel.SetActive(false);
        startButton.gameObject.SetActive(true);  // Показати кнопку Start
        settingsButton.gameObject.SetActive(true); // Показати кнопку Settings
        exitButton.gameObject.SetActive(true);    // Показати кнопку Exit
        UpdateMessage("Game about the cubes");
        StartCoroutine(AnimateButtons());
    }

    void ExitGame()
    {
        Application.Quit();
        UpdateMessage("Вихід з гри...");
    }

    void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreen);
        UpdateMessage($"Роздільну здатність змінено на {width}x{height}");
    }

    void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        string fullscreenStatus = isFullscreen ? "увімкнено" : "вимкнено";
        UpdateMessage($"Режим Fullscreen {fullscreenStatus}");
    }

    // Функція для оновлення повідомлень на екрані
    void UpdateMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }
}
