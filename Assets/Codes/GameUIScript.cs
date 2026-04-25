using System.Collections;
using DG.Tweening;
// using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIScript : MonoBehaviour
{


    [SerializeField] FadingPanel pauseFadingPanel;
    [SerializeField] CanvasGroup pauseMenuCanvas;

    [SerializeField] FadingPanel storyFadingPanel;

    [SerializeField] float duration;

    [SerializeField] FadeTransition fadeTransition;

    [SerializeField] GameObject player;

    [SerializeField] TheEndScript theEndScript;

    [SerializeField] CanvasGroup optionCanvasGroup;

    public static bool hasShownMenu = false;

    public static Vector3? savedPosition = null;

    [SerializeField] MouseLockToggle mouseLockToggle;

    public void BackToMenuButton()
    {
        StartCoroutine(ToMainMenu());
    }

    IEnumerator ToMainMenu()
    {
        savedPosition = player.transform.position;
        fadeTransition.Transition(0.5f);
        yield return new WaitForSeconds(0.51f);
        SceneManager.LoadScene(0);
    }

    void Start()
    {
        if (storyFadingPanel != null && !hasShownMenu)
        {
            StartCoroutine(WaitAndShow());
            hasShownMenu = true;        
        } 

        if (savedPosition.HasValue)
        {
            player.transform.position = savedPosition.Value;
        }
        else
        {
           // player.transform.position = new Vector3(1f, -0.2f, -4f);
        }
    }

    IEnumerator WaitAndShow()
    {
        yield return new WaitForSeconds(0.2f);
        storyFadingPanel.FadeIn(duration);
        mouseLockToggle.SetCursorState(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuCanvas.alpha < 0.01 && optionCanvasGroup.alpha == 0)
        {
            pauseFadingPanel.FadeIn(duration);
            mouseLockToggle.SetCursorState(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuCanvas.alpha > 0.99 && optionCanvasGroup.alpha == 0)
        {
            pauseFadingPanel.FadeOut(duration);
            mouseLockToggle.SetCursorState(true);
        }
    }

    public void HidePanel(FadingPanel panel)
    {
        panel.FadeOut(duration);
        mouseLockToggle.SetCursorState(true);
    }

    public void ShowPanel(FadingPanel panel)
    {
        panel.FadeIn(duration);
        mouseLockToggle.SetCursorState(false);
    }
}
