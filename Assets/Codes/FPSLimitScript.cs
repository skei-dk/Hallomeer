using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections; // Це не використовується, але ви його мали

public class FPSLimitScript : MonoBehaviour
{
    int targetFPS;
    TMP_InputField inputFieldFPS;
    public static FPSLimitScript Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            // Якщо дублікат, знищуємо його
            Destroy(gameObject);
            return; // Важливо вийти з Awake, щоб не виконувати код нижче
        }

        // Завантажуємо FPS при першому запуску гри
        if (PlayerPrefs.HasKey("FPS"))
        {
            LoadFPS();
        }
        else
        {
            // Встановлюємо значення за замовчуванням, якщо нічого не збережено
            targetFPS = 60; // Наприклад
            ApplyFPS();
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ця функція тепер викликається КОЖНОГО разу при завантаженні сцени
        FindTargetObject();
    }

    private void FindTargetObject()
    {
        TMP_InputField[] allFields = FindObjectsOfType<TMP_InputField>(true);
        TMP_InputField foundInput = allFields.FirstOrDefault(input => input.gameObject.CompareTag("FPSInput"));

        if (foundInput != null)
        {
            inputFieldFPS = foundInput;

            // 1. Очищуємо старих слухачів (на випадок перезавантаження сцени)
            inputFieldFPS.onValueChanged.RemoveAllListeners();

            // 2. Додаємо слухача ТУТ, коли об'єкт точно знайдено
            inputFieldFPS.onValueChanged.AddListener(ChangeFPS);

            // 3. Оновлюємо текст у полі, щоб він відповідав завантаженому значенню
            inputFieldFPS.text = targetFPS.ToString();
        }
        else
        {
            inputFieldFPS = null;
        }
    }

    // Ця функція викликається слухачем onValueChanged
    private void ChangeFPS(string textFPS)
    {
        // Використовуємо TryParse для безпечного перетворення
        if (int.TryParse(textFPS, out int newFPS))
        {
            targetFPS = newFPS;
            ApplyFPS(); // Застосовуємо налаштування
            PlayerPrefs.SetInt("FPS", targetFPS); // Зберігаємо
        }
    }

    // Завантажує FPS з пам'яті
    private void LoadFPS()
    {
        targetFPS = PlayerPrefs.GetInt("FPS");
        ApplyFPS(); // Застосовуємо налаштування
    }

    // Виніс логіку застосування FPS в окремий метод
    private void ApplyFPS()
    {
        StartCoroutine(ApplyinfFPS());
    }

    IEnumerator ApplyinfFPS()
    {

        yield return new WaitForSeconds(2f);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
        
    }
}