using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreManager_KJS : MonoBehaviour
{
    public int baseAmount = 100000;
    public float penaltyPerSecond = 1500f;
    public float maxTime = 60f;

    private float elapsedTime = 0f;
    private int finalAmount = 0;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI resultText;
    public GameObject resultPanel;

    private Coroutine hideCoroutine;

    void Start()
    {
        // ✅ 씬 전환 후 이전 데이터 복원
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

        // 남은 시간 출력
        if (timerText != null)
        {
            int min = Mathf.FloorToInt(remaining / 60f);
            int sec = Mathf.FloorToInt(remaining % 60f);
            timerText.text = $"{min:00}:{sec:00}";
        }

        // 점수 자동 계산
        finalAmount = Mathf.Max(0, Mathf.FloorToInt(baseAmount - elapsedTime * penaltyPerSecond));
    }

    public void FinishScoring()
    {
        // ✅ 점수 및 시간 저장
        ScoreDataCarrier_KJS.Instance.FinalScore = finalAmount;
        ScoreDataCarrier_KJS.Instance.ElapsedTime = elapsedTime;

        Debug.Log($"✅ 저장 완료: {finalAmount}점 / {elapsedTime:0.00}초");

        // ✅ 결과 출력
        ShowResultUI();
    }

    private void ShowResultUI()
    {
        if (resultText != null)
            resultText.text = $"Money: ₩{finalAmount:N0}";

        if (resultPanel != null)
            resultPanel.SetActive(true);

        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(HideResultPanelAfterDelay());

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator HideResultPanelAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        if (resultPanel != null)
            resultPanel.SetActive(false);
    }
}

