using UnityEngine;

public class PersistentToggle : MonoBehaviour
{
    // Посилання на об'єкт, який ми вмикаємо/вимикаємо
    // (на кожній сцені сюди треба перетягнути СВІЙ об'єкт FPS)
    public GameObject objectToToggle;

    // Ключ, за яким ми зберігаємо стан. 
    // Головне, щоб він був ОДНАКОВИЙ для обох скриптів.
    private string preferenceKey = "isFpsCounterVisible";

    void Start()
    {
        // Цей код виконується при завантаженні сцени
        LoadState();
    }

    // Метод, який викликає ваша кнопка
    public void ToggleVisibility()
    {
        if (objectToToggle != null)
        {
            // 1. Перемикаємо поточний стан
            bool newState = !objectToToggle.activeSelf;
            objectToToggle.SetActive(newState);

            // 2. Зберігаємо цей новий стан
            SaveState(newState);
        }
    }

    private void LoadState()
    {
        // Перевіряємо, чи є взагалі такий запис у PlayerPrefs
        if (PlayerPrefs.HasKey(preferenceKey))
        {
            // Завантажуємо збережений стан (1 = true, 0 = false)
            int savedState = PlayerPrefs.GetInt(preferenceKey);
            bool isVisible = (savedState == 1); // Конвертуємо int назад у bool

            // Застосовуємо стан до об'єкта
            if (objectToToggle != null)
            {
                objectToToggle.SetActive(isVisible);
            }
        }
        else
        {
            // Якщо збереження немає (гравець ще нічого не натискав)
            // Ми просто беремо поточний стан об'єкта (який ви виставили в редакторі)
            // і зберігаємо його як стан за замовчуванням
            if (objectToToggle != null)
            {
                SaveState(objectToToggle.activeSelf);
            }
        }
    }

    private void SaveState(bool isVisible)
    {
        // Конвертуємо bool (true/false) в int (1/0)
        int stateToSave = isVisible ? 1 : 0;
        
        // Зберігаємо значення за нашим унікальним ключем
        PlayerPrefs.SetInt(preferenceKey, stateToSave);
        PlayerPrefs.Save(); // Примусово зберігаємо зміни на диск
        
    }
}