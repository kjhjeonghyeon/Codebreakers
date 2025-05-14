using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SequenceChecker : MonoBehaviour
{
    [Tooltip("정답으로 요구되는 순서대로 BlockID를 설정하세요.")]
    public List<string> correctSequence = new List<string>
    {
        "1",
        "2",
        "3"
    };

    [Tooltip("결과를 보여줄 텍스트(선택사항)")]
    public TMP_Text resultText;

    [Tooltip("완료 버튼")]
    public Button submitButton;

    private void Start()
    {
        // 버튼 클릭에 연결
        if (submitButton != null)
            submitButton.onClick.AddListener(OnSubmitByPosition);
    }

    private void OnDestroy()
    {
        if (submitButton != null)
            submitButton.onClick.RemoveListener(OnSubmitByPosition);
    }

    /// <summary>
    /// 씬에 있는 모든 Block을 Y 좌표 내림차순(위→아래)으로 정렬해서 ID 순서 리스트를 만들고 비교
    /// </summary>
    public void OnSubmitByPosition()
    {
        // 1) 모든 Block 찾기
        Block[] blocks = FindObjectsOfType<Block>();

        // 2) Y 좌표 기준 정렬
        //    - 위에서 아래로: Y 값이 큰 순서 => OrderByDescending
        //    - 아래에서 위로: OrderBy(b => b.transform.position.y)
        List<string> userSequence = blocks
            .OrderByDescending(b => b.transform.position.y)
            .Select(b => b.BlockNum)
            .ToList();

        // 3) 정답 비교
        bool isCorrect = userSequence.SequenceEqual(correctSequence);

        // 4) 결과 출력
        if (isCorrect)
        {
            Debug.Log("정답! 순서가 일치합니다.");
            if (resultText != null) resultText.text = " 정답입니다!";
        }
        else
        {
            Debug.Log("오답… 순서를 다시 확인하세요.");
            if (resultText != null) resultText.text = " 순서가 일치하지 않습니다.";

            // (디버깅용) 실제 유저 리스트와 정답 리스트 로깅
            Debug.Log("유저 순서: " + string.Join(", ", userSequence));
            Debug.Log("정답 순서: " + string.Join(", ", correctSequence));
        }
    }
}
