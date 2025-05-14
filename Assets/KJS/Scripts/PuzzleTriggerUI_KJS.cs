using System.Collections;
using TMPro;
using UnityEngine;

public class PuzzleTriggerUI_KJS : MonoBehaviour
{
    public GameObject uiPanel;
    public PlayerMove_KJS playerMovementScript;
    public CameraMove cameraScript;
    public ScoreManager_KJS scoreManager;
    public SequenceChecker sequenceChecker; // ✅ SequenceChecker 연결
    public TextMeshProUGUI infoText; // 오답 안내만 표시

    private bool hasStartedScoring = false;

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
            lastPuzzleObject = other.gameObject; // ✅ 여기 저장

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

        // ✅ 정답일 경우: 접촉한 퍼즐만 비활성화
        if (lastPuzzleObject != null)
        {
            Debug.Log($"✅ 정답 → 퍼즐 오브젝트 비활성화: {lastPuzzleObject.name}");
            lastPuzzleObject.SetActive(false);
        }

        if (uiPanel != null)
            uiPanel.SetActive(false);

        if (playerMovementScript != null)
            playerMovementScript.enabled = true;

        if (cameraScript != null)
            cameraScript.enabled = true;

        LockCursor(true);

        if (scoreManager != null)
            scoreManager.FinishScoring();
    }

    private IEnumerator HideInfoTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (infoText != null)
            infoText.gameObject.SetActive(false);
    }
}

