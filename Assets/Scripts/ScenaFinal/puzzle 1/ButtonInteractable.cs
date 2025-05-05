using UnityEngine;

public class ButtonInteractable : MonoBehaviour
{
    public int buttonID;
    public Material defaultMaterial;
    public Material highlightMaterial;
    public Material activatedMaterial;
    public float detectionRadius = 2.0f;

    private MeshRenderer meshRenderer;
    private bool isPlayerNear = false;
    private PuzzleManager puzzleManager;
    private Transform playerTransform;
    private bool isHighlighted = false;
    private bool hasBeenPressed = false;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        puzzleManager = FindObjectOfType<PuzzleManager>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        if (meshRenderer && defaultMaterial)
        {
            meshRenderer.material = defaultMaterial;
        }

        if (puzzleManager != null)
        {
            puzzleManager.RegistrarBoton(this);
        }
    }

    void Update()
    {
        if (playerTransform == null || hasBeenPressed) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        bool playerIsNearNow = distanceToPlayer <= detectionRadius;

        if (playerIsNearNow != isPlayerNear)
        {
            isPlayerNear = playerIsNearNow;

            if (isPlayerNear)
            {
                if (meshRenderer && highlightMaterial && !isHighlighted)
                {
                    meshRenderer.material = highlightMaterial;
                    isHighlighted = true;
                }
            }
            else
            {
                if (meshRenderer && defaultMaterial && isHighlighted)
                {
                    meshRenderer.material = defaultMaterial;
                    isHighlighted = false;
                }
            }
        }

        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            PressButton();
        }
    }

    private void PressButton()
    {
        if (meshRenderer && activatedMaterial)
        {
            meshRenderer.material = activatedMaterial;
        }

        hasBeenPressed = true;

        if (puzzleManager != null)
        {
            puzzleManager.ButtonPressed(buttonID);
        }
    }

    public void ResetToDefaultMaterial()
    {
        if (meshRenderer && defaultMaterial)
        {
            meshRenderer.material = defaultMaterial;
        }

        isHighlighted = false;
        hasBeenPressed = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
