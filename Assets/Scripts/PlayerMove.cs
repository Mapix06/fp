using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3.0f;
    public float runSpeed = 7.0f;
    public float speedRotation = 200.0f;

    private CharacterController controller;
    private Animator anim;
    private float gravity = -9.81f;
    private float verticalVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        // Asegurarse de que el objeto tiene el tag "Player"
        if (gameObject.tag != "Player")
        {
            gameObject.tag = "Player";
            Debug.Log("Se ha asignado automáticamente el tag 'Player' al objeto del jugador");
        }
    }

    void Update()
    {
        HandleMovement();
        // La interacción se gestiona en los objetos interactuables por proximidad
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        transform.Rotate(0, x * Time.deltaTime * speedRotation, 0);
        Vector3 moveDirection = transform.forward * y * currentSpeed;

        if (controller.isGrounded)
        {
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity;
        controller.Move(moveDirection * Time.deltaTime);

        if (anim != null)
        {
            float movementAmount = Mathf.Abs(y);
            float speedParam = movementAmount > 0.1f ? (isRunning ? 1f : 0.5f) : 0f;
            anim.SetFloat("Speed", speedParam);
            anim.SetFloat("Strafe", x);
        }
    }
}