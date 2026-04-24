using System.Data.Common;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { private set; get; }

    [Header("----------Sources----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }


    public void PlayClick()
    {
        SFXSource.Play();
    }
}
