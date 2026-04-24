    using Unity.VisualScripting;
    using UnityEngine;

    public class StopMovingScript : MonoBehaviour
    {

        Vector3 destination;
        PlatformMover component;

        private Vector3 checkPoint;
        void Start()
        {
            PlatformMover platformMover = gameObject.GetComponent<PlatformMover>();
            destination = destination = transform.parent.TransformPoint(platformMover.pointB);

            component = gameObject.GetComponent<PlatformMover>();
            checkPoint.x = -12.072f;
            checkPoint.y = 58.65f;
            checkPoint.z = 181.552f;
        }

        void Update()
        {
            if (Vector3.Distance(transform.position, destination) < 0.1f)
            {
                component.enabled = false;
                CheckPointScript.vectorPoint = checkPoint;
            }
        }
    }
