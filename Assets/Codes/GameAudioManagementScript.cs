using UnityEngine;

public class GameAudioManagementScript : MonoBehaviour
{
    AudioManager gameAudioManager;

    void Awake()
    {
        gameAudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void PlayClickButton()
    {
        gameAudioManager.PlayClick();
    }
}
