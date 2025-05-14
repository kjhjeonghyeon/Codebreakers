using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager_KJS : MonoBehaviour
{
    public int baseAmount = 2000000000;
    public float penaltyPerSecond = 300000f;
    public float maxTime = 60f;
    private bool hasSceneChanged = false;
    public string nextSceneName = "Scene 5"; // ⬅️ 이동할 씬 이름 지정

    private float elapsedTime = 0f;
    private int finalAmount = 0;
    private bool isTimerRunning = false; // ✅ 타이머 제어용

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

        // ✅ 인트로 패널이 없다면 자동으로 타이머 시작
        bool hasIntroPanel = GameObject.FindObjectOfType<IntroPannel_KJS>() != null;
        if (!hasIntroPanel)
        {
            StartTimer();
        }
    }


    void Update()
    {
        if (!isTimerRunning) return; // ✅ 타이머가 켜지기 전엔 동작 금지

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

        if (remaining <= 0f && !hasSceneChanged)
        {
            hasSceneChanged = true;
            StartCoroutine(TransitionAfterDelay(1f)); // 감성적 연출 타이밍
        }
    }
    public void StartTimer()
    {
        isTimerRunning = true;
        Debug.Log("▶️ 타이머 시작됨");
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

    private IEnumerator TransitionAfterDelay(float delay)
    {
        // 🎬 여기에 사운드, 애니메이션, 화면 페이드 넣어도 좋음
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }
}

