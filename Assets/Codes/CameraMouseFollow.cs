using UnityEngine;

public class CameraMouseFollow : MonoBehaviour
{
    [Header("Settings")]
    public float maxOffset = 2f;
    public float smoothSpeed = 5f;

    private Vector3 initialPosition;
    private Vector2 smoothedInput;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Нормалізуємо мишку в 0..1
        float rawX = Input.mousePosition.x / Screen.width;
        float rawY = Input.mousePosition.y / Screen.height;

        // Обмежуємо (щоб не виходило за межі)
        rawX = Mathf.Clamp01(rawX);
        rawY = Mathf.Clamp01(rawY);

        // Перетворюємо в -1..1
        Vector2 targetInput = new Vector2(rawX * 2f - 1f, rawY * 2f - 1f);

        // Плавимо вхід, щоб не було постійних мікро-змін
        smoothedInput = Vector2.Lerp(smoothedInput, targetInput, Time.deltaTime * 5f);

        // Якщо майже досягли краю — “залипаємо”
        if (Mathf.Abs(targetInput.x) > 0.99f)
            smoothedInput.x = Mathf.Sign(targetInput.x);

        if (Mathf.Abs(targetInput.y) > 0.99f)
            smoothedInput.y = Mathf.Sign(targetInput.y);

        Vector3 targetOffset = new Vector3(smoothedInput.x, smoothedInput.y, 0f) * maxOffset;

        Vector3 targetPosition = initialPosition + targetOffset;

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            Time.deltaTime * smoothSpeed
        );
    }
}