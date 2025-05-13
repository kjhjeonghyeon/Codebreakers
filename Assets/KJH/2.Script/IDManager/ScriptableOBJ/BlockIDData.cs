using UnityEngine;

[CreateAssetMenu(menuName = "Block/ID Data", fileName = "New BlockIDData")]
public class BlockIDData : ScriptableObject
{
    [Tooltip("��: \"Input\", \"Output\", \"PlayerStart\" ��")]
    public string id;
    [Tooltip("��: \"1\", \"2\", \"3\" ��")]
    public int num;
}
