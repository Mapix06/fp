using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
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
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Rotar al personaje
        transform.Rotate(0, x * Time.deltaTime * speedRotation, 0);

        // Calcular dirección hacia adelante
        Vector3 moveDirection = transform.forward * y * currentSpeed;

        // Aplicar gravedad
        if (controller.isGrounded)
        {
            verticalVelocity = -1f; // mantener pegado al suelo
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity;

        // Mover usando CharacterController
        controller.Move(moveDirection * Time.deltaTime);

        // Animaciones
        float movementAmount = Mathf.Abs(y);
        float speedParam = movementAmount > 0.1f ? (isRunning ? 1f : 0.5f) : 0f;

        anim.SetFloat("Speed", speedParam);
        anim.SetFloat("Strafe", x);
    }
}
