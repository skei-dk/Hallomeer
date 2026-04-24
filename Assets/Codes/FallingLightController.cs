using StarterAssets;
using UnityEngine;

public class FallingLightController : MonoBehaviour
{
    public Rigidbody playerRb;
    public Light spotLight;

    public ThirdPersonController controller;

    public float fallThreshold = -5f;
    public float smoothSpeed = 5f;

    [Header("Intensity")]
    public float minIntensity = 0.3f;
    private float originalIntensity;

    [Header("Range")]
    public float minRange = 3f;
    private float originalRange;

    [Header("Spot Angle")]
    public float minSpotAngle = 30f;
    private float originalSpotAngle;

    void Start()
    {
        originalIntensity = spotLight.intensity;
        originalRange = spotLight.range;
        originalSpotAngle = spotLight.spotAngle;
    }

    void Update()
    {
        float verticalSpeed = playerRb.linearVelocity.y;
        bool isFalling = !controller.Grounded && verticalSpeed < fallThreshold;
        

        float targetIntensity = isFalling ? minIntensity : originalIntensity;
        float targetRange = isFalling ? minRange : originalRange;
        float targetAngle = isFalling ? minSpotAngle : originalSpotAngle;

        spotLight.intensity = Mathf.Lerp(spotLight.intensity, targetIntensity, Time.deltaTime * smoothSpeed);
        spotLight.range = Mathf.Lerp(spotLight.range, targetRange, Time.deltaTime * smoothSpeed);
        spotLight.spotAngle = Mathf.Lerp(spotLight.spotAngle, targetAngle, Time.deltaTime * smoothSpeed);
    }
}