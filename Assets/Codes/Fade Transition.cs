using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    public Image transitionImage;

    private void Start()
    {
        StartCoroutine(FadeOut(2f));
    }

    public void Transition(float duration)
    {
        StartCoroutine(FadeIn(duration));
    }


    IEnumerator FadeIn(float duration)
    {
        float t = 0;
        Color c = transitionImage.color;
        
        while (t < duration)
        {
            t += Time.deltaTime;
            c.a = t / duration;
            transitionImage.color = c;
            yield return null;
        }
    }


    IEnumerator FadeOut(float duration)
    {
        float t = 0;
        Color c = transitionImage.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            c.a = 1f - (t / duration);
            transitionImage.color = c;
            yield return null;
        }
    }
}
