using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public FadingPanel mainMenu;
    public FadingPanel optionsMenu;

    [SerializeField] FadeTransition fadeTransition;

    [SerializeField] Button playButton;
    [SerializeField] TMP_Text playButtonText;

    public float duration = 0.5f;

    bool isTracing = false;

    void Start()
    {
        if (GameFiles.gameFinished)
        {
            playButton.interactable = false;
            playButtonText.text = ". . .";
        }
    }

    public void OpenOptions()
    {
        if (isTracing) return;

        isTracing = true;

        mainMenu.FadeOut(duration, ()=>
        {
            optionsMenu.FadeIn(duration);
            isTracing = false;
        });
    }

    public void OpenMainMenu()
    {
        if (isTracing) return;

        isTracing = true;

        optionsMenu.FadeOut(duration, ()=>
        {
            mainMenu.FadeIn(duration);
            isTracing = false;
        });
    }

    public void PlayButton()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        fadeTransition.Transition(0.5f);
        yield return new WaitForSeconds(0.51f);
        SceneManager.LoadScene(1);
    }
    
    public void QuitButton()
    {
        Application.Quit(); 
    }



}
