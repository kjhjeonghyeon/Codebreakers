using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;                     // �÷��̾� ������Ʈ (CameraTarget ���� ����)
    public Vector3 offset = new Vector3(0f, 2f, -5f); // ī�޶� ��ġ ������
    public float rotationSpeed = 5f;

    private float currentX = 0f;
    private float currentY = 0f;
    public float yMin = -35f;
    public float yMax = 60f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // ���콺 �Է����� ȸ�� �� ����
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currentY = Mathf.Clamp(currentY, yMin, yMax);

        // ȸ�� ���
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        // ȸ���� ������ ��ġ ���
        Vector3 desiredPosition = player.position + rotation * offset;
        transform.position = desiredPosition;

        // �÷��̾� �ٶ󺸱�
        transform.LookAt(player.position);
    }
}
