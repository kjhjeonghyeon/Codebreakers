using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager_KJS : MonoBehaviour
{
    public int baseAmount = 100000;
    public float penaltyPerSecond = 1500f;

    public float maxTime = 60f;  // 최대 제한 시간
    private float elapsedTime = 0f;
    private bool isScoring = false;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI gradeText;

    private int finalAmount = 0;
    private string grade = "F";

    void Start()
    {
        if (resultPanel != null)
            resultPanel.SetActive(false);
    }

    void Update()
    {
        if (isScoring)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= maxTime)
            {
                FinishScoring();
            }
        }
    }

    public void StartScoring()
    {
        elapsedTime = 0f;
        isScoring = true;
    }

    public void FinishScoring()
    {
        isScoring = false;

        // 점수 계산
        finalAmount = Mathf.Max(0, Mathf.FloorToInt(baseAmount - elapsedTime * penaltyPerSecond));
        grade = EvaluateGrade(finalAmount);

        ShowResultUI();
    }

    private string EvaluateGrade(int amount)
    {
        if (amount >= 90000) return "S";
        else if (amount >= 70000) return "A";
        else if (amount >= 50000) return "B";
        else return "FAIL";
    }

    private void ShowResultUI()
    {
        if (resultPanel != null)
        {
            resultPanel.SetActive(true);
            resultText.text = $"Money: ₩{finalAmount:N0}";
            gradeText.text = $"Ranking: {grade}";

            // ✅ 자동 종료 코루틴 실행
            StartCoroutine(HideResultPanelAfterDelay());
        }

        // ✅ 커서는 보여주되, 컨트롤은 이미 복원되어 있다고 가정
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator HideResultPanelAfterDelay()
    {
        yield return new WaitForSeconds(5f); // 5초 대기

        if (resultPanel != null)
            resultPanel.SetActive(false);
    }

}
