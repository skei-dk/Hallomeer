using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour
{
    public Animator animator;
    public Light chestLight;

    private bool playerNear = false;
    private bool opened = false;

    void Update()
    {
        if (playerNear && !opened)
        {
            animator.SetTrigger("OpenChest");
            StartCoroutine(FadeLight());
            opened = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerNear = false;
        }
    }

    IEnumerator FadeLight()
    {
        chestLight.enabled = true;

        float time = 0;
        float duration = 2f;
        float startIntensity = chestLight.intensity;

        while (time < duration)
        {
            chestLight.intensity = Mathf.Lerp(startIntensity, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        chestLight.enabled = false;
    }
}
