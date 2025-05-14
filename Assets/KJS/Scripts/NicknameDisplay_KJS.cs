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
        // ������ ó��: ī�޶� �������� Canvas ȸ��
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.position);
    }
}
