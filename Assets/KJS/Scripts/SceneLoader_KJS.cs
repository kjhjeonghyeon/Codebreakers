using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class SceneLoader_KJS : MonoBehaviour
{
    public string rankURL = "http://localhost:8080/users/rank";

    public void LoadMainScene()
    {
        // 점수 업데이트 요청 후 씬 전환
        StartCoroutine(SendScoreAndLoadScene());
    }

    private IEnumerator SendScoreAndLoadScene()
    {
        // 값 꺼내기
        string nickname = PlayerPrefs.GetString("Nickname", "unknown");
        int assets = ScoreDataCarrier_KJS.Instance != null ? ScoreDataCarrier_KJS.Instance.FinalScore : 0;

        // 요청 URL 만들기
        string requestUrl = $"{rankURL}?nickname={UnityWebRequest.EscapeURL(nickname)}&assets={assets}";
        Debug.Log("📡 랭킹 서버로 전송: " + requestUrl);

        UnityWebRequest request = UnityWebRequest.Get(requestUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("✅ 점수 전송 성공");
        }
        else
        {
            Debug.LogWarning("⚠️ 점수 전송 실패: " + request.error);
        }

        // 씬 전환
        SceneManager.LoadScene("MainScene");
    }
}


