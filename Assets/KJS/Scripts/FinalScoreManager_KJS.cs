using System.Collections;
using UnityEngine;
using TMPro;

public class FinalScoreManager_KJS : MonoBehaviour
{
    public int baseAmount = 100000;
    public float penaltyPerSecond = 1500f;
    public float maxTime = 60f;

    private float elapsedTime = 0f;
    private int finalAmount = 0;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI gradeText; // ✅ 등급 출력용 추가
    public GameObject resultPanel;

    private Coroutine hideCoroutine;

    void Start()
    {
        // ✅ 이전 점수 불러오기
        if (ScoreDataCarrier_KJS.Instance != null)
        {
            elapsedTime = ScoreDataCarrier_KJS.Instance.ElapsedTime;
            finalAmount = ScoreDataCarrier_KJS.Instance.FinalScore;

            Debug.Log($"📥 ScoreManager 초기화됨 - 시간: {elapsedTime}, 점수: {finalAmount}");
        }

        if (resultPanel != null)
            resultPanel.SetActive(false);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > maxTime)
            elapsedTime = maxTime;

        float remaining = maxTime - elapsedTime;

        if (timerText != null)
        {
            int min = Mathf.FloorToInt(remaining / 60f);
            int sec = Mathf.FloorToInt(remaining % 60f);
            timerText.text = $"{min:00}:{sec:00}";
        }

        finalAmount = Mathf.Max(0, Mathf.FloorToInt(baseAmount - elapsedTime * penaltyPerSecond));
    }

    public void FinishScoring()
    {
        ScoreDataCarrier_KJS.Instance.FinalScore = finalAmount;
        ScoreDataCarrier_KJS.Instance.ElapsedTime = elapsedTime;

        Debug.Log($"✅ 저장 완료: {finalAmount}점 / {elapsedTime:0.00}초");

        ShowResultUI();
    }

    private void ShowResultUI()
    {
        if (resultText != null)
            resultText.text = $"Money: ₩{finalAmount:N0}";

        if (gradeText != null)
            gradeText.text = $"Ranking: {EvaluateGrade(finalAmount)}"; // ✅ 등급 표시

        if (resultPanel != null)
            resultPanel.SetActive(true);

        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(HideResultPanelAfterDelay());

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private string EvaluateGrade(int amount)
    {
        if (amount >= 90000) return "S";
        else if (amount >= 70000) return "A";
        else if (amount >= 50000) return "B";
        else return "FAIL";
    }

    private IEnumerator HideResultPanelAfterDelay()
    {
        yield return new WaitForSeconds(5f);

        if (resultPanel != null)
            resultPanel.SetActive(false);
    }
}


