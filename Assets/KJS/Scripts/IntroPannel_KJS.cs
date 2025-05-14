using System.Collections;
using UnityEngine;

public class IntroPannel_KJS : MonoBehaviour
{
    public GameObject introPanel;
    public ScoreManager_KJS scoreManager;
    public float introDuration = 3f;

    void Start()
    {
        introPanel.SetActive(true); // 인트로 패널 켜기
        if (scoreManager.timerText != null)
            scoreManager.timerText.gameObject.SetActive(false); // ⛔ 타이머 텍스트 숨기기

        StartCoroutine(HideIntroAndStart());
    }

    IEnumerator HideIntroAndStart()
    {
        yield return new WaitForSeconds(introDuration); // 3초 대기

        introPanel.SetActive(false); // 인트로 패널 끄기
        if (scoreManager.timerText != null)
            scoreManager.timerText.gameObject.SetActive(true); // ✅ 타이머 텍스트 다시 켜기

        scoreManager.StartTimer();   // 타이머 시작
    }
}

