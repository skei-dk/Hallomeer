using System.Collections;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class RotatingScript : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpped = 140f;
    [Header("Knockback Settings")]
    public float knockBackForce = 10f;
    public float verticalBoost = 0f;


    void Update()
    {
        transform.Rotate(Vector3.right * rotationSpped * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerKnockback player))
        {
            Vector3 dir = (collision.transform.position - transform.position).normalized;

            dir.y = +verticalBoost;

            player.ApplyKnockback(dir, knockBackForce);
            StartCoroutine(ResetKnockBackAfterDelay(player, 1f));
        }
    }

    IEnumerator ResetKnockBackAfterDelay(PlayerKnockback player, float time)
    {
        yield return new WaitForSeconds(time);
        player.ResetKnockback();
    }
}
