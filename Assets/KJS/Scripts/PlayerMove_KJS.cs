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

    public Animator animator;               // �� Inspector�� ������ Animator
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. �� ����
        isGrounded = Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + 0.1f, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 2. �Է� (ī�޶� ���� �̵� ���� ���)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Transform cam = Camera.main.transform;
        Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized; // ���� ���⸸
        Vector3 move = (camForward * v + cam.right * h).normalized;

        // 2.5 �޸��� �Է� üũ
        bool runInput = Input.GetKey(KeyCode.LeftShift) && move.magnitude > 0.1f;
        animator.SetBool("isRun", runInput);   // �� Animator �Ķ���� ����

        // 3. �̵�
        controller.Move(move * moveSpeed * Time.deltaTime);
       

        // 4. �÷��̾� ���� ��ȯ
        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // 5. ����
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 6. �߷� ����
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}





