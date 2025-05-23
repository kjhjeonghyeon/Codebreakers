﻿using System.Collections;
using UnityEngine;
using TMPro;

public class IntroPannel_KJS : MonoBehaviour
{
    [Header("UI")]
    public GameObject introPanel;
    public TextMeshProUGUI introText;
    public ScoreManager_KJS scoreManager;

    [Header("Typing")]
    [TextArea(5, 10)]
    public string fullText =
        "세계 최대 금융 기업, 메타은행(MetaBank)...\n\n" +
        "그들은 알고리즘으로 자산을 통제하고, 데이터를 착취하며 세계를 지배해왔다.\n\n" +
        "하지만— 그들의 시스템에 균열을 낼 자들이 나타났다.\n\n" +
        "코드 브레이커즈(Code Breakers).\n\n" +
        "우리는 규칙을 깨고, 코드를 뚫고, 정의를 되찾는다.\n\n" +
        "오늘 밤, 그들의 금고는 열린다.";

    public float typingSpeed = 0.03f;

    void Start()
    {
        introPanel.SetActive(true);
        introText.text = "";

        if (scoreManager.timerText != null)
            scoreManager.timerText.gameObject.SetActive(false);

        StartCoroutine(TypeTextAndStartTimer());
    }

    IEnumerator TypeTextAndStartTimer()
    {
        foreach (char c in fullText)
        {
            introText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(0.5f); // 텍스트 다 나오고 잠깐 쉬고

        introPanel.SetActive(false); // 패널 끄기
        if (scoreManager.timerText != null)
            scoreManager.timerText.gameObject.SetActive(true); // 타이머 텍스트 보이기

        scoreManager.StartTimer(); // 타이머 시작
    }
}


