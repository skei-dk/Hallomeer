using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class FallingPlatform : MonoBehaviour
{
    [Header("Timing")]
    public float delayToCrack = 1f;     // час до тріщин
    public float delayToFall = 1f;      // час до падіння
    public float respawnTime = 3f;      // час до респавну

    [Header("Visuals & Sound")]
    public Material crackedMaterial;
    public AudioClip crackSound;

    [Header("Settings")]
    public string playerTag = "Player";
    // внутрішні
    Material originalMaterial;
    MeshRenderer meshRenderer;
    Collider col;
    AudioSource audioSource;
    Coroutine sequenceCoroutine;
    bool isProcessing = false;
    Vector3 startPosition;
    Quaternion startRotation;

    Rigidbody rb;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
            meshRenderer = GetComponentInChildren<MeshRenderer>();

        col = GetComponent<Collider>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        if (meshRenderer != null)
            originalMaterial = meshRenderer.material;

        // запам'ятовуємо стартову позицію
        startPosition = transform.position;
        startRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();

        rb.isKinematic = true;
        rb.useGravity = false;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (isProcessing) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            TryStartSequence();
        }
    }

    void TryStartSequence()
    {
        sequenceCoroutine = StartCoroutine(CrackThenFall());
    }

    IEnumerator CrackThenFall()
    {
        isProcessing = true;

        // 1) чекати до тріску
        yield return new WaitForSeconds(delayToCrack);

        if (meshRenderer != null && crackedMaterial != null)
            meshRenderer.material = crackedMaterial;

        if (crackSound != null && audioSource != null)
            audioSource.PlayOneShot(crackSound);

        // 2) чекати до падіння
        yield return new WaitForSeconds(delayToFall);

        // Робимо платформу фізично рухомою
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.isKinematic = false;
        rb.useGravity = true;

        // чекаємо respawnTime перед респавном
        yield return new WaitForSeconds(respawnTime);

        rb.isKinematic = true;
        rb.useGravity = false;
        // 3) респавн: відновлюємо початкову позицію та стан
        transform.position = startPosition;
        transform.rotation = startRotation;

        if (meshRenderer != null)
        {
            meshRenderer.material = originalMaterial;
            meshRenderer.enabled = true;
        }

        if (col != null) col.enabled = true;

        isProcessing = false;
    }
}
