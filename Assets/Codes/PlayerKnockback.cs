using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerKnockback : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 knockbackDir;
    private float knockbackSpeed;
    private float knockbackDecay = 5f; // наскільки швидко згасає поштовх

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (knockbackSpeed > 0)
        {
            Vector3 motion = knockbackDir * knockbackSpeed * Time.deltaTime;
            controller.Move(motion + Physics.gravity * Time.deltaTime);
            knockbackSpeed = Mathf.Lerp(knockbackSpeed, 0, Time.deltaTime * knockbackDecay);
        }
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        knockbackDir = direction.normalized;
        knockbackSpeed = force;
    }
    public void ResetKnockback()
    {
        knockbackSpeed = 0f;
        knockbackDir = Vector3.zero;
    }

}
