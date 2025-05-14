using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    public AudioClip musicClip;        // 인스펙터에 할당할 음악 클립

    private static BackgroundMusic instance;
    private AudioSource audioSource;

    void Awake()
    {
        // 싱글턴 인스턴스 체크
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);   // 씬 전환 시 삭제 방지
        }
        else if (instance != this)
        {
            Destroy(gameObject);            // 이미 인스턴스가 있으면 자신은 파괴
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = true;            // 반복 재생
    }

    void Start()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}
