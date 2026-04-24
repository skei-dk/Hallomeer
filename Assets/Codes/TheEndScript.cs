using UnityEngine;
using UnityEngine.InputSystem;

public class TheEndScript : MonoBehaviour
{
    [SerializeField] FadingPanel endFadingPanel;
    [SerializeField] FadingPanel endImage;
    [SerializeField] MouseLockToggle mouseLockToggle;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mouseLockToggle.SetCursorState(false);
            endFadingPanel.FadeIn(1.5f);
            endImage.FadeIn(1.5f);
            GameFiles.gameFinished = true;
        }
    }
}
