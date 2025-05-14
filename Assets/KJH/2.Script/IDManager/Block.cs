using UnityEngine;

public class Block : MonoBehaviour
{
    [Tooltip("미리 만들어둔 BlockIDData 에셋을 드래그하세요")]
    public BlockIDData idData;

    // 외부에 읽기 전용으로 노출
    public string BlockID => idData != null ? idData.id : "<None>";
    public string BlockNum => idData != null ? idData.num : "<None>";

    private void Awake()
    {
        if (idData == null)
            Debug.LogWarning($"[{name}] idData가 할당되지 않았습니다.");
        else
            Debug.Log($"[{name}] BlockID = {BlockID}");


    }
}
