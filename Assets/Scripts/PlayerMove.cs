using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 3.0f;
    public float runSpeed = 7.0f;
    public float speedRotation = 200.0f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        transform.Rotate(0, x * Time.deltaTime * speedRotation, 0);
        transform.Translate(0, 0, y * Time.deltaTime * currentSpeed);

        // Animaciones de movimiento
        float movementAmount = Mathf.Abs(y);
        float speedParam = 0f;

        if (movementAmount > 0.1f)
            speedParam = isRunning ? 1f : 0.5f;

        anim.SetFloat("Speed", speedParam);
        anim.SetFloat("Strafe", x);
    }
}
