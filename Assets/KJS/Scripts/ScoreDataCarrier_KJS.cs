using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreDataCarrier_KJS : MonoBehaviour
{
    public static ScoreDataCarrier_KJS Instance;

    public int FinalScore;
    public float ElapsedTime;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
