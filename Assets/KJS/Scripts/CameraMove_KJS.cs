using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;                     // 플레이어 오브젝트 (CameraTarget 없이 직접)
    public Vector3 offset = new Vector3(0f, 2f, -5f); // 카메라 위치 오프셋
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

        // 마우스 입력으로 회전 값 누적
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currentY = Mathf.Clamp(currentY, yMin, yMax);

        // 회전 계산
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        // 회전된 오프셋 위치 계산
        Vector3 desiredPosition = player.position + rotation * offset;
        transform.position = desiredPosition;

        // 플레이어 바라보기
        transform.LookAt(player.position);
    }
}
