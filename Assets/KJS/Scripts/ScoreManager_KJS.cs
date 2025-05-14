using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreManager_KJS : MonoBehaviour
{
    public int baseAmount = 2000000000;
    public float penaltyPerSecond = 300000f;
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
        Debug.Log($"🧪 Start() 시점 baseAmount = {baseAmount}, penalty = {penaltyPerSecond}");

        if (ScoreDataCarrier_KJS.Instance != null && ScoreDataCarrier_KJS.Instance.hasScoreBeenSet)
        {
            elapsedTime = ScoreDataCarrier_KJS.Instance.ElapsedTime;
            Debug.Log($"📥 점수 복원 생략, 시간만 복원: {elapsedTime}");
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
        ScoreDataCarrier_KJS.Instance.FinalScore = finalAmount;
        ScoreDataCarrier_KJS.Instance.ElapsedTime = elapsedTime;
        ScoreDataCarrier_KJS.Instance.hasScoreBeenSet = true; // ✅ 플래그 설정

        Debug.Log($"✅ 저장 완료: {finalAmount}점 / {elapsedTime:0.00}초");

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

