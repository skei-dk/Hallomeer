using System.Collections;
using UnityEngine;

public class FadeTrigger : MonoBehaviour
{
    [SerializeField] private FadeObjectGroup targetGroupToFade;
    [SerializeField] private GameObject targetGroup;
    [SerializeField] private FadeObjectGroup targetGroupToBecome;
    [SerializeField] private GameObject targetGroupToBe;
    [SerializeField] private bool fadeOutOnEnter = true;
    private bool coroutineIsWorking = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && targetGroupToFade != null && targetGroupToBecome != null)
        {
            if (fadeOutOnEnter)
            {
                StartCoroutine(FadeSequence(true, 2f));
            }
            else
            {
                StartCoroutine(FadeSequence(false, 2f));
            }
        }
    }

    private IEnumerator FadeSequence(bool fadeOut, float time)
    {
        if (coroutineIsWorking) yield break;
        coroutineIsWorking = true;
        if (fadeOut)
        {
            targetGroupToFade.FadeOut();
            yield return new WaitForSeconds(time); // чекаємо завершення
            targetGroup.SetActive(false);

            PrepareForFadeIn(targetGroupToBe);
            targetGroupToBe.SetActive(true);

            targetGroupToBecome.FadeIn();

        }
        else
        {
            targetGroupToFade.FadeIn();
            yield return new WaitForSeconds(time);
            targetGroup.SetActive(true);

            targetGroupToBecome.FadeOut();
            yield return new WaitForSeconds(time);
            targetGroupToBe.SetActive(false);
        }
    }


    private void PrepareForFadeIn(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer r in renderers)
        {
            foreach (Material mat in r.materials)
            {
                if (mat.HasProperty("_Color"))
                {
                    Color c = mat.color;
                    c.a = 0.2f; // робимо прозорим
                    mat.color = c;
                }
            }
        }
    }

}
