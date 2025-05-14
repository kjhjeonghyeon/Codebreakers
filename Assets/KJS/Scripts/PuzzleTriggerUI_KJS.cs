using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PuzzleTriggerUI_KJS : MonoBehaviour
{
    public GameObject uiPanel;
    public PlayerMove_KJS playerMovementScript;
    public CameraMove cameraScript;
    public ScoreManager_KJS scoreManager;
    public SequenceChecker sequenceChecker;
    public TextMeshProUGUI infoText;

    [Tooltip("정답일 경우 이동할 씬 이름")]
    public string nextSceneName = "NextScene"; // ✅ 인스펙터에서 설정

    private GameObject lastPuzzleObject;

    void Start()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);

        if (infoText != null)
            infoText.gameObject.SetActive(false);

        LockCursor(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puzzle"))
        {
            lastPuzzleObject = other.gameObject;

            if (uiPanel != null)
                uiPanel.SetActive(true);

            if (playerMovementScript != null)
                playerMovementScript.enabled = false;

            if (cameraScript != null)
                cameraScript.enabled = false;

            LockCursor(false);
        }
    }

    void LockCursor(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

    public void ClosePuzzleUI()
    {
        if (sequenceChecker == null)
        {
            Debug.LogWarning("SequenceChecker가 연결되어 있지 않습니다.");
            return;
        }

        bool isCorrect = sequenceChecker.OnSubmitByPosition();

        if (!isCorrect)
        {
            if (infoText != null)
            {
                infoText.text = "순서가 올바르지 않습니다.";
                infoText.gameObject.SetActive(true);
                StartCoroutine(HideInfoTextAfterDelay(3f));
            }
            return;
        }

        // ✅ 정답일 경우 점수 출력 먼저
        if (scoreManager != null)
            scoreManager.FinishScoring(); // 내부에서 resultPanel UI 자동 출력

        // ✅ 씬 전환 준비
        if (lastPuzzleObject != null)
        {
            var sceneTarget = lastPuzzleObject.GetComponent<PuzzleSceneTarget_KJS>();
            if (sceneTarget != null && !string.IsNullOrEmpty(sceneTarget.targetSceneName))
            {
                Debug.Log($"✅ 정답 → 점수 출력 후 씬 이동 예약: {sceneTarget.targetSceneName}");
                StartCoroutine(DelayedSceneLoad(sceneTarget.targetSceneName, 1f)); // 5초 지연 후 씬 이동
                return;
            }
        }

        // 예외 상황 처리 (씬 정보가 없을 경우)
        if (uiPanel != null)
            uiPanel.SetActive(false);

        if (playerMovementScript != null)
            playerMovementScript.enabled = true;

        if (cameraScript != null)
            cameraScript.enabled = true;

        LockCursor(true);
    }

    private IEnumerator DelayedSceneLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator HideInfoTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (infoText != null)
            infoText.gameObject.SetActive(false);
    }
}

