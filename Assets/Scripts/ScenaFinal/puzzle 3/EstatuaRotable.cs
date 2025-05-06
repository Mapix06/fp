using UnityEngine;

[RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody))]
public class EstatuaRotable : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("ID único (0-3) que determina su posición en la solución")]
    public int estatuaID = 0;
    [Range(1f, 10f)] public float detectionRadius = 2.5f;
    public KeyCode interactionKey = KeyCode.E;

    [Header("Feedback")]
    public ParticleSystem rotationParticles;
    public AudioClip rotationSound;
    public Material highlightMaterial;

    private float currentRotation = 0f;
    private Transform player;
    private PuzzleRotacionManager manager;
    private AudioSource audioSource;
    private bool playerInRange = false;
    private Material originalMaterial;
    private Renderer statueRenderer;

    void Start()
    {
        ConfigurePhysicsComponents();
        GetReferences();
        RegisterWithManager();
        StoreOriginalMaterials();
    }

    void ConfigurePhysicsComponents()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void GetReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        statueRenderer = GetComponentInChildren<Renderer>();
    }

    void RegisterWithManager()
    {
        manager = FindObjectOfType<PuzzleRotacionManager>();
        if (manager != null) manager.RegistrarEstatua(this);
    }

    void StoreOriginalMaterials()
    {
        if (statueRenderer != null) originalMaterial = statueRenderer.material;
    }

    void Update()
    {
        if (player == null) return;

        CheckPlayerDistance();
        HandlePlayerInput();
    }

    void CheckPlayerDistance()
    {
        float distancia = Vector3.Distance(transform.position, player.position);
        bool nowInRange = distancia <= detectionRadius;

        if (nowInRange && !playerInRange) PlayerEnteredRange();
        if (!nowInRange && playerInRange) PlayerExitedRange();
    }

    void PlayerEnteredRange()
    {
        playerInRange = true;
        if (statueRenderer != null && highlightMaterial != null)
            statueRenderer.material = highlightMaterial;
    }

    void PlayerExitedRange()
    {
        playerInRange = false;
        if (statueRenderer != null && originalMaterial != null)
            statueRenderer.material = originalMaterial;
    }

    void HandlePlayerInput()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            RotarEstatua();
        }
    }

    public void RotarEstatua()
    {
        currentRotation = (currentRotation + 90f) % 360f;
        transform.rotation = Quaternion.Euler(0, currentRotation, 0);

        PlayRotationEffects();
        NotifyManager();
    }

    void PlayRotationEffects()
    {
        if (rotationParticles != null) rotationParticles.Play();
        if (rotationSound != null) audioSource.PlayOneShot(rotationSound);
    }

    void NotifyManager()
    {
        if (manager != null) manager.ValidarPuzzle();
    }

    public float GetAnguloActual()
    {
        // Devuelve la rotación normalizada (0, 90, 180 o 270)
        return currentRotation;
    }

    public int GetID()
    {
        return estatuaID;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}