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
    private readonly List<Block> placedBlocks = new List<Block>();

    public void RegisterBlock(Block block)
    {
        if (!placedBlocks.Contains(block))
            placedBlocks.Add(block);
    }

    public void UnregisterBlock(Block block)
    {
        placedBlocks.Remove(block);
    }

    /// <summary>
    /// 외부에서 호출할 수 있도록 bool 반환 메서드
    /// </summary>
    public bool OnSubmitByPosition()
    {
        Debug.Log("▶ [OnSubmitByPosition 호출 확인] => " + Time.frameCount);

        List<string> userSequence = placedBlocks
            .OrderByDescending(b => b.transform.position.y) // 또는 입력 순서로 유지
            .Select(b => b.BlockNum)
            .ToList();

        bool isCorrect = userSequence.SequenceEqual(correctSequence);

        Debug.Log(isCorrect ? "정답!" : "오답!");

        if (!isCorrect)
        {
            Debug.Log("유저 순서: " + string.Join(", ", userSequence));
            Debug.Log("정답 순서: " + string.Join(", ", correctSequence));
        }

        return isCorrect;
    }
}



