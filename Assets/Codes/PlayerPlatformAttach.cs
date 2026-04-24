using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovingPlatformAttach : MonoBehaviour
{
    private PlatformMover currentPlatform;
    private CharacterController controller;
    public Vector3 PlatformVelocity { get; private set; }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.TryGetComponent(out PlatformMover platform))
        {
            // стоїмо зверху
            if (Vector3.Dot(hit.normal, Vector3.up) > 0.5f)
            {
                currentPlatform = platform;
            }
        }
        else
        {
            currentPlatform = null;
        }
    }

    void FixedUpdate()
    {
        if (currentPlatform != null && controller.isGrounded && currentPlatform.enabled)
            PlatformVelocity = currentPlatform.DeltaPosition / Time.fixedDeltaTime;
        else
            PlatformVelocity = Vector3.zero;
    }
}
