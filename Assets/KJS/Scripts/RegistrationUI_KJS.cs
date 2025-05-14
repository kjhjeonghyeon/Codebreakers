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
        int maxRetry = 3;
        int attempt = 0;
        bool success = false;

        while (attempt < maxRetry && !success)
        {
            attempt++;

            using (UnityWebRequest request = new UnityWebRequest(registerURL, "POST"))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.timeout = 10; // 타임아웃 10초

                Debug.Log($"📡 [시도 {attempt}/{maxRetry}] 서버로 전송 중...");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("✅ 서버 전송 성공: " + request.downloadHandler.text);
                    success = true;
                    yield break;
                }
                else
                {
                    Debug.LogWarning($"⚠️ 서버 전송 실패 (시도 {attempt}): {request.error}");

                    // 연결 불가 or timeout 같은 경우 재시도, 그 외는 중단
                    if (request.result == UnityWebRequest.Result.ConnectionError ||
                        request.result == UnityWebRequest.Result.ProtocolError ||
                        request.result == UnityWebRequest.Result.DataProcessingError)
                    {
                        yield return new WaitForSeconds(2f); // 재시도 전 대기
                    }
                    else
                    {
                        break; // 치명적 오류는 반복하지 않음
                    }
                }
            }
        }

        if (!success)
        {
            Debug.LogError("❌ 모든 서버 전송 시도 실패");
        }
    }
}


