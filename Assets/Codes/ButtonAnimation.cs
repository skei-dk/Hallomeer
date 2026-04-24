using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private Vector3 targetScale;

    [SerializeField] float multiplicationScale = 1.1f;
    [SerializeField] float animationTime = 0.1f;

    private float timer;
    private Vector3 startScale;
    private bool isAnimating;

    void Awake()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartAnimation(originalScale * multiplicationScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartAnimation(originalScale);
    }

    void StartAnimation(Vector3 newTarget)
    {
        startScale = transform.localScale; // важливо!
        targetScale = newTarget;
        timer = 0f;
        isAnimating = true;
    }

    void Update()
    {
        if (!isAnimating) return;

        timer += Time.deltaTime;
        float t = timer / animationTime;
        t = Mathf.SmoothStep(0, 1, t);

        transform.localScale = Vector3.Lerp(startScale, targetScale, t);

        if (t >= 1f)
        {
            transform.localScale = targetScale;
            isAnimating = false;
        }
    }
}