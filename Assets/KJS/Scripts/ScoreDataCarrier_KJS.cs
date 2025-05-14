using UnityEngine;

public class ScoreDataCarrier_KJS : MonoBehaviour
{
    public static ScoreDataCarrier_KJS Instance;

    public int FinalScore;
    public float ElapsedTime;
    public bool hasScoreBeenSet = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 제거
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Clear()
    {
        FinalScore = 0;
        ElapsedTime = 0f;
        hasScoreBeenSet = false;
    }
}

