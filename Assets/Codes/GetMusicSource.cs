using UnityEngine;

public class GetMusicSource : MonoBehaviour
{
    [SerializeField] AudioSource targetMusicSource;
    [SerializeField] AudioSource thisMusicSource;

    [SerializeField] AudioSource targetSFXSource;
    [SerializeField] AudioSource thisSFXSource;

    void Start()
    {
        if (targetMusicSource != null && thisMusicSource != null)
        {
            CopyingSources(targetMusicSource, thisMusicSource);
        }
        if (targetSFXSource != null && thisSFXSource != null)
        {
            CopyingSources(targetSFXSource, thisSFXSource);
        }
    }

    private void CopyingSources(AudioSource from, AudioSource to)
    {
        to.clip = from.clip;
        to.volume = from.volume;
    }

    
}
