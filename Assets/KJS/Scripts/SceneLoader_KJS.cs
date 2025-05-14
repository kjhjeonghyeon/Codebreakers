using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader_KJS : MonoBehaviour
{
    public void LoadMainScene()
    {
        // ✅ 점수 및 시간 리셋
        if (ScoreDataCarrier_KJS.Instance != null)
        {
            ScoreDataCarrier_KJS.Instance.Clear();
            Debug.Log("🔁 ScoreDataCarrier 초기화 완료");
        }

        // ✅ 씬 이동
        SceneManager.LoadScene("MainScene");
    }
}

