using UnityEngine;

public class CheckThePlayerScriptForAward : MonoBehaviour
{
    [SerializeField] GameObject gameAward;

    [SerializeField] FadingPanel awardFadingPanel;
    [SerializeField] MouseLockToggle mouseLockToggle;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameAward.SetActive(true);
            mouseLockToggle.SetCursorState(false);
            awardFadingPanel.FadeIn(0.45f);
        }
    }
}
