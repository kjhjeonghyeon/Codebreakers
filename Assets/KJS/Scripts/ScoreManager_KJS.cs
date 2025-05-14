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

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private Coroutine hideCoroutine;

    public TextMeshProUGUI timerText; // ← Timer 텍스트 연결용

    void Start()
    {
        if (resultPanel != null)
            resultPanel.SetActive(false);
    }

    void Update()
    {
        // ⏱️ 시간은 항상 흐름 (씬 시작부터)
        elapsedTime += Time.deltaTime;

        // 안전 장치: 최대 시간 초과되면 고정
        if (elapsedTime > maxTime)
            elapsedTime = maxTime;

        // 🕒 타이머 표시
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    public void FinishScoring()
    {
        // 💸 현재 시점의 점수 계산
        finalAmount = Mathf.Max(0, Mathf.FloorToInt(baseAmount - elapsedTime * penaltyPerSecond));

        ShowResultUI();
    }

    private void ShowResultUI()
    {
        if (resultText != null)
            resultText.text = $"Money: ₩{finalAmount:N0}";

        if (resultPanel != null)
            resultPanel.SetActive(true);

        // 중복 코루틴 방지
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

