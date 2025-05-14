using UnityEngine;
using TMPro;

public class RegistrationUI_KJS : MonoBehaviour
{
    [Header("UI 오브젝트")]
    public GameObject loginPanel;         // ← Login 전체 오브젝트
    public GameObject registrationPanel;  // ← 회원가입 패널

    [Header("입력 필드")]
    public TMP_InputField inputNickname;
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;

    // 회원가입 화면 열기
    public void OpenRegistration()
    {
        if (registrationPanel != null)
            registrationPanel.SetActive(true);

        if (loginPanel != null)
            loginPanel.SetActive(false);
    }

    // 가입 완료 버튼 클릭 시
    public void SubmitRegistration()
    {
        string nickname = inputNickname.text;
        string email = inputEmail.text;
        string password = inputPassword.text;

        RegistrationData jsonData = new RegistrationData
        {
            nickname = nickname,
            email = email,
            password = password
        };

        string json = JsonUtility.ToJson(jsonData, true);
        Debug.Log("회원가입 JSON 데이터:\n" + json);
    }
}


