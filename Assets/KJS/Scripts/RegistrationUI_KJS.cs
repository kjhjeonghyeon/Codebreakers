using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class RegistrationUI_KJS : MonoBehaviour
{
    [Header("UI 오브젝트")]
    public GameObject loginPanel;
    public GameObject registrationPanel;

    [Header("입력 필드")]
    public TMP_InputField inputNickname;
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;

    [Header("서버 주소")]
    public string registerURL = "https://your.api/register"; // ✅ 실제 API 주소로 변경

    [System.Serializable]
    public class RegistrationData
    {
        public string nickname;
        public string email;
        public string password;
    }

    public void OpenRegistration()
    {
        if (registrationPanel != null)
            registrationPanel.SetActive(true);
        if (loginPanel != null)
            loginPanel.SetActive(false);
    }

    public void SubmitRegistration()
    {
        RegistrationData data = new RegistrationData
        {
            nickname = inputNickname.text,
            email = inputEmail.text,
            password = inputPassword.text
        };

        string json = JsonUtility.ToJson(data);
        Debug.Log("전송할 JSON:\n" + json);

        StartCoroutine(PostRegistrationData(json));
    }

    private IEnumerator PostRegistrationData(string jsonData)
    {
        UnityWebRequest request = new UnityWebRequest(registerURL, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("✅ 서버 전송 성공: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("❌ 서버 전송 실패: " + request.error);
        }
    }
}


