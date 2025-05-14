using UnityEngine;
using TMPro;

public class RegistrationUI_KJS : MonoBehaviour
{
    [Header("UI ������Ʈ")]
    public GameObject loginPanel;         // �� Login ��ü ������Ʈ
    public GameObject registrationPanel;  // �� ȸ������ �г�

    [Header("�Է� �ʵ�")]
    public TMP_InputField inputNickname;
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;

    // ȸ������ ȭ�� ����
    public void OpenRegistration()
    {
        if (registrationPanel != null)
            registrationPanel.SetActive(true);

        if (loginPanel != null)
            loginPanel.SetActive(false);
    }

    // ���� �Ϸ� ��ư Ŭ�� ��
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
        Debug.Log("ȸ������ JSON ������:\n" + json);
    }
}


