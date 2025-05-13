using UnityEngine;

public class PlayerMove_KJS : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpHeight = 3.5f;
    public float gravity = -30f;

    public LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. 이동 입력
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // 2. 바닥 레이어 감지 (Raycast)
        isGrounded = Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + 0.1f, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 3. 점프
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 4. 중력 적용
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}




