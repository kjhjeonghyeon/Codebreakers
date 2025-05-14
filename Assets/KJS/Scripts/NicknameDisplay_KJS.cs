using UnityEngine;
using TMPro;

public class NicknameDisplay : MonoBehaviour
{
    public TextMeshProUGUI nicknameText;
    private Transform mainCam;

    void Start()
    {
        mainCam = Camera.main.transform;

        string nickname = PlayerPrefs.GetString("Nickname", "Unknown");
        nicknameText.text = nickname;
    }

    void LateUpdate()
    {
        // 빌보드 처리: 카메라 방향으로 Canvas 회전
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.position);
    }
}
