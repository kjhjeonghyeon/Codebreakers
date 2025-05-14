using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class LoginManager_KJS : MonoBehaviour
{
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;

    [Header("API 주소")]
    public string loginURL = "https://your.api/login"; // ← 실제 API 주소로 수정

    public void OnLoginButtonClicked()
    {
        string email = inputEmail.text;
        string password = inputPassword.text;

        StartCoroutine(SendLoginRequest(email, password));
    }

    private IEnumerator SendLoginRequest(string email, string password)
    {
        // URL 쿼리 인코딩
        string url = $"{loginURL}?email={UnityWebRequest.EscapeURL(email)}&password={UnityWebRequest.EscapeURL(password)}";
        Debug.Log($"📡 로그인 요청 URL: {url}");

        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("✅ 로그인 성공:\n" + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("❌ 로그인 실패: " + request.error);
        }
    }
}

