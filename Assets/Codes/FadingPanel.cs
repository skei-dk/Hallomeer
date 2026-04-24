using System.Threading.Tasks.Sources;
using DG.Tweening;
using UnityEngine;

public class FadingPanel : MonoBehaviour
{
 
 [SerializeField] CanvasGroup canvasGroup;

 private Tween fadeTween;

 public void FadeIn(float duration, TweenCallback onComplete = null)
    {
        Fade(1f, duration, () =>
        {
            onComplete?.Invoke();
        });
    }

 public void FadeOut(float duration, TweenCallback onComplete = null)
    {
        canvasGroup.blocksRaycasts = false;

        Fade(0f, duration, onComplete);
    }

 private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if (fadeTween != null) fadeTween.Kill(false);

        fadeTween = canvasGroup.DOFade(endValue, duration);

        fadeTween.OnUpdate(() =>
        {
            if (endValue == 1f && canvasGroup.alpha > 0.75f)
            {
                canvasGroup.blocksRaycasts = true;
            }
        });
        fadeTween.onComplete += onEnd;
    }
}
