using UnityEngine;

public class ExitApp_KJS : MonoBehaviour
{
    public void QuitApplication()
    {
        Debug.Log("🛑 종료 요청됨");

        // 에디터에서 종료 테스트
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
