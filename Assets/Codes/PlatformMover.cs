using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2f;

    private bool movingToB = true;

    private Vector3 lastPos;
    public Vector3 DeltaPosition { get; private set; }

    void Start()
    {
        lastPos = transform.position;
    }
    void FixedUpdate()
    {
        // рух між A і B
        Vector3 target = movingToB ? transform.parent.TransformPoint(pointB) : transform.parent.TransformPoint(pointA);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);

        // якщо дійшли до однієї точки — змінюємо напрям
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            movingToB = !movingToB;
        }
        DeltaPosition = transform.position - lastPos;
        lastPos = transform.position;
    }

}
