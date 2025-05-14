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

    public Animator animator;               // ① Inspector에 연결할 Animator
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. 땅 감지
        isGrounded = Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + 0.1f, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 2. 입력 (카메라 기준 이동 방향 계산)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Transform cam = Camera.main.transform;
        Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized; // 수평 방향만
        Vector3 move = (camForward * v + cam.right * h).normalized;

        // 2.5 달리기 입력 체크
        bool runInput = Input.GetKey(KeyCode.LeftShift) && move.magnitude > 0.1f;
        animator.SetBool("isRun", runInput);   // ② Animator 파라미터 세팅

        // 3. 이동
        controller.Move(move * moveSpeed * Time.deltaTime);
       

        // 4. 플레이어 방향 전환
        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // 5. 점프
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 6. 중력 적용
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}





