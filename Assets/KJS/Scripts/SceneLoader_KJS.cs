using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader_KJS : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene"); // 씬 이름 정확히 일치해야 함
    }
}
