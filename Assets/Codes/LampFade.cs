using UnityEngine;

public class LampFade : MonoBehaviour
{
    [Header("Light Settings")]
    public Light lampLight;
    public float maxIntensity = 5f;
    public float fadeSpeed = 2f;

    [Header("FireFlies Settings")]

    public ParticleSystem fireFlies;

    [Header("Flicker Settings (Noise Based)")]
    public bool enableFlicker = true;
    public float flickerStrength = 0.3f;
    public float noiseSpeed = 1.5f;

    [Header("Trigger Settings")]
    public string playerTag = "Player";

    private float targetIntensity = 0f;
    
    // НОВА ЗМІННА: Зберігає "чисту" яскравість без шуму
    private float currentBaseIntensity = 0f; 
    
    private float noiseOffset;

    private void Reset()
    {
        lampLight = GetComponentInChildren<Light>();
    }

    private void Start()
    {
        if (lampLight != null)
        {
            lampLight.intensity = 0f;
            currentBaseIntensity = 0f; // Ініціалізація
        }

        noiseOffset = Random.Range(0f, 9999f);
    }

    private void Update()
    {
        if (lampLight == null) return;

        // 1. Змінюємо нашу внутрішню змінну, а не саме світло
        // Це гарантує стабільність переходу незалежно від шуму
        currentBaseIntensity = Mathf.MoveTowards(currentBaseIntensity, targetIntensity, Time.deltaTime * fadeSpeed * maxIntensity);

        if (enableFlicker && currentBaseIntensity > 0.01f)
        {
            float noise = Mathf.PerlinNoise(noiseOffset, Time.time * noiseSpeed);
            // Шум тепер відцентрований відносно 0
            float flicker = (noise - 0.5f) * flickerStrength; 
            
            // 2. Присвоюємо лампі значення: База + Шум
            // Важливо: Mathf.Max(0, ...), щоб яскравість не стала від'ємною (чорною дірою)
            lampLight.intensity = Mathf.Max(0f, currentBaseIntensity + flicker);
        }
        else
        {
            lampLight.intensity = currentBaseIntensity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            targetIntensity = maxIntensity;
            fireFlies.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            targetIntensity = 0f;
            fireFlies.Stop();
        }
    }
}