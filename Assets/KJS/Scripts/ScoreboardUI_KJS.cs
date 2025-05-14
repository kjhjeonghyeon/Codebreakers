using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class ScoreboardUI_KJS : MonoBehaviour
{
    public string rankURL = "http://172.16.16.246:8080/users/rank";
    public Transform contentParent; // ScrollView > Content
    public GameObject rankEntryPrefab; // 프리팹: Text 2개 (닉네임, 자산)

    void OnEnable()
    {
        Debug.Log("📡 OnEnable() 진입 - 랭킹 요청 시작");
        StartCoroutine(FetchRankingData());
    }

    IEnumerator FetchRankingData()
    {
        Debug.Log("▶ 랭킹 데이터 전송 및 요청 시작");

        // ✅ 점수와 닉네임을 ScoreDataCarrier에서 가져옴
        string nickname = PlayerPrefs.GetString("Nickname", "unknown");
        int assets = ScoreDataCarrier_KJS.Instance != null ? ScoreDataCarrier_KJS.Instance.FinalScore : 0;

        string requestUrl = $"{rankURL}?nickname={UnityWebRequest.EscapeURL(nickname)}&assets={assets}";
        UnityWebRequest request = UnityWebRequest.Get(requestUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            Debug.Log("📦 받은 JSON: " + json);

            RankData[] rankList = JsonHelper.FromJson<RankData>(json);
            Debug.Log($"👥 받은 랭커 수: {rankList.Length}");

            int rank = 1;
            foreach (var data in rankList)
            {
                Debug.Log($"👤 생성: {rank}위 - {data.nickname} ({data.assets}G)");

                GameObject entry = Instantiate(rankEntryPrefab, contentParent);
                entry.transform.localScale = Vector3.one;

                var texts = entry.GetComponentsInChildren<TextMeshProUGUI>();
                if (texts.Length >= 2)
                {
                    texts[0].text = $"{rank}. - {data.nickname}";
                    texts[1].text = $"{data.assets:N0} G";
                }
                else
                {
                    Debug.LogWarning("⚠️ 프리팹에 TextMeshProUGUI가 부족함");
                }

                rank++;
            }
        }
        else
        {
            Debug.LogError("❌ 랭킹 요청 실패: " + request.error);
        }
    }
}

