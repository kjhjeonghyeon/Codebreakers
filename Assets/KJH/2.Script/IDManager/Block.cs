using UnityEngine;

public class Block : MonoBehaviour
{
    [Tooltip("�̸� ������ BlockIDData ������ �巡���ϼ���")]
    public BlockIDData idData;

    // �ܺο� �б� �������� ����
    public string BlockID => idData != null ? idData.id : "<None>";
    public string BlockNum => idData != null ? idData.num : "<None>";

    private void Awake()
    {
        if (idData == null)
            Debug.LogWarning($"[{name}] idData�� �Ҵ���� �ʾҽ��ϴ�.");
        else
            Debug.Log($"[{name}] BlockID = {BlockID}");


    }
}
