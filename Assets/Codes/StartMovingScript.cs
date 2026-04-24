using System.Collections;
using UnityEngine;

public class StartMovingScript : MonoBehaviour
{
    private bool moving = false;
    private bool coroutineRunning = false;

    private Vector3 startPosition;
    private PlatformMover platformMover;

    [SerializeField] private float stopDistance = 0.05f;

    void Awake()
    {
        startPosition = transform.position;
        platformMover = GetComponent<PlatformMover>();
        platformMover.enabled = false;
    }

    void FixedUpdate()
    {
        if (moving &&
            Vector3.Distance(transform.position, startPosition) < stopDistance)
        {
            platformMover.enabled = false;
            moving = false;
            coroutineRunning = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (moving || coroutineRunning) return;

        platformMover.enabled = true;
        StartCoroutine(StartMovingAfterDelay());
    }

    private IEnumerator StartMovingAfterDelay()
    {
        coroutineRunning = true;
        yield return new WaitForSeconds(4f);
        moving = true;
    }
}
