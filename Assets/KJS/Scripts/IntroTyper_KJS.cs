using System.Collections;
using UnityEngine;
using TMPro;

public class IntroTyper_KJS : MonoBehaviour
{
    [Header("UI References")]
    public GameObject introPanel;
    public TextMeshProUGUI introText;
    public GameObject skipButton; // (선택)

    [Header("Typing Settings")]
    [TextArea(5, 10)]
    public string fullText =
        "세계 최대 금융 기업, 메타은행(MetaBank)...\n\n" +
        "그들은 알고리즘으로 자산을 통제하고, 데이터를 착취하며 세계를 지배해왔다.\n\n" +
        "하지만— 그들의 시스템에 균열을 낼 자들이 나타났다.\n\n" +
        "코드 브레이커즈(Code Breakers).\n\n" +
        "우리는 규칙을 깨고, 코드를 뚫고, 정의를 되찾는다.\n\n" +
        "오늘 밤, 그들의 금고는 열린다.";

    public float typingSpeed = 0.03f; // 글자당 간격

    void Start()
    {
        introPanel.SetActive(true);
        skipButton?.SetActive(false);
        introText.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char c in fullText)
        {
            introText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        // 다 출력된 후 버튼 활성화 (또는 자동 종료 가능)
        if (skipButton != null)
            skipButton.SetActive(true);
    }

    // 버튼 클릭 시 호출 (혹은 TypeText() 끝에 자동 종료 가능)
    public void CloseIntro()
    {
        introPanel.SetActive(false);
        Debug.Log("✅ 인트로 종료");
    }
}
