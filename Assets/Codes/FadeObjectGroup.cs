using UnityEngine;
using System.Collections;

public class FadeObjectGroup : MonoBehaviour
{
    [Header("Основні параметри")]
    [SerializeField] private float fadeSpeed = 0.5f;

    [Header("Матеріал для фейду (Fade або Transparent Mode)")]
    [SerializeField] private Material fadeMaterial;

    private Material[] originalMaterials;
    private Material[] activeMaterials;
    private Renderer[] renderers;
    private bool isFadingOut = false;
    private bool isFadingIn = false;
    private bool isFadingNow = false; // ← ось це буде справжній "замок"
    private bool hasFadeOut = false;
    private bool hasFadeIn = false;


    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[renderers.Length];
        activeMaterials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].material;
            activeMaterials[i] = originalMaterials[i];
        }
    }

    void Update()
    {
        if (isFadingOut)
            FadeTo(0f);
        else if (isFadingIn)
            FadeTo(1f);
    }

    private void FadeTo(float targetAlpha)
    {
        foreach (Material mat in activeMaterials)
        {
            if (mat.HasProperty("_Color"))
            {
                Color color = mat.color;
                color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
                mat.color = color;

            }
        }

        if (Mathf.Approximately(activeMaterials[0].color.a, targetAlpha))
        {
            isFadingOut = false;
            isFadingIn = false;
            isFadingNow = false; // ← розблоковуємо, коли завершено
        }
    }

    public void FadeOut()
    {
        if (hasFadeOut == true) return;
        hasFadeOut = true;
        if (isFadingNow) return; // ← блокуємо повторний запуск
        isFadingNow = true;

        ApplyFadeMaterials();
        foreach (var mat in activeMaterials)
        {
            mat.DisableKeyword("_FOG_ON");
        }

        isFadingOut = true;
    }

    public void FadeIn()
    {
        if (hasFadeIn == true) return;
        hasFadeIn = true;

        if (isFadingNow) return;
        isFadingNow = true;

        if (activeMaterials == null || activeMaterials.Length == 0)
            Initialize();

        ApplyFadeMaterials();
        foreach (var mat in activeMaterials)
        {
            mat.DisableKeyword("_FOG_ON");
        }

        SetAllToZero();
        isFadingIn = true;
        StartCoroutine(ReturnOriginalAfterFade());
    }

    private void ApplyFadeMaterials()
    {
        if (fadeMaterial == null) return;

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = new Material(fadeMaterial);
            activeMaterials[i] = renderers[i].material;
            activeMaterials[i].EnableKeyword("_FOG_ON");
        }
    }

    private IEnumerator ReturnOriginalAfterFade()
    {
        yield return new WaitUntil(() => Mathf.Approximately(activeMaterials[0].color.a, 1f));

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = originalMaterials[i];
            activeMaterials[i] = originalMaterials[i];
        }
    }

    public void Initialize()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
        activeMaterials = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            activeMaterials[i] = renderers[i].material;
        }
    }

    private void SetAllToZero()
    {
        foreach (Material mat in activeMaterials)
        {
            if (mat.HasProperty("_Color"))
            {
                Color color = mat.color;
                color.a = 0.2f;
                mat.color = color;
            }
        }
        
    }
}
