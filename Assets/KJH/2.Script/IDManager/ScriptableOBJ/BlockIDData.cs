using UnityEngine;

[CreateAssetMenu(menuName = "Block/ID Data", fileName = "New BlockIDData")]
public class BlockIDData : ScriptableObject
{
    [Tooltip("예: \"Input\", \"Output\", \"PlayerStart\" 등")]
    public string id;
    [Tooltip("예: \"1\", \"2\", \"3\" 등")]
    public int num;
}
