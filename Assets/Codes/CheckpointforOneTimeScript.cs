using StarterAssets;
using UnityEngine;

public class CheckpointforOneTimeScript : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] ThirdPersonController controller;

    [SerializeField] ParticleSystem particles;

    [SerializeField] ParticleSystem shockWave;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && controller.Grounded == true)
        {
            CheckPointScript.vectorPoint = player.transform.position;
            particles.Play();
            shockWave.Play();
            Destroy(gameObject);
        }
    }
}
