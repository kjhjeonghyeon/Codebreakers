using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    public AudioClip musicClip;        // �ν����Ϳ� �Ҵ��� ���� Ŭ��

    private static BackgroundMusic instance;
    private AudioSource audioSource;

    void Awake()
    {
        // �̱��� �ν��Ͻ� üũ
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);   // �� ��ȯ �� ���� ����
        }
        else if (instance != this)
        {
            Destroy(gameObject);            // �̹� �ν��Ͻ��� ������ �ڽ��� �ı�
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = true;            // �ݺ� ���
    }

    void Start()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}
