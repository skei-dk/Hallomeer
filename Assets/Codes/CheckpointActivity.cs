using UnityEngine;
using System.Collections;


public class CheckpointActivity : MonoBehaviour
{
    [SerializeField] GameObject fadeObject;
    [SerializeField] GameObject becomingObject;

    public Light flashLight;

    public ParticleSystem SparkBurst;

    public ParticleSystem FloatingGlow;

    private bool IsItPlayed = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            fadeObject.SetActive(false);
            becomingObject.SetActive(true);
            if (!IsItPlayed)
            {
                SparkBurst.Play();
                FloatingGlow.Play();
                flashLight.enabled = true;
                IsItPlayed = true;
            }
            
        } 

    }

}
