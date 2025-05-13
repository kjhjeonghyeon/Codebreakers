using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzleTriggerUI_KJS : MonoBehaviour
{
    public GameObject uiPanel;
    public PlayerMove_KJS playerMovementScript;
    public CameraMove cameraScript;
    public ScoreManager_KJS scoreManager;
    private bool hasStartedScoring = false;
    public bool isScorable = true; // 퍼즐별 점수화 여부 결정
    public TextMeshProUGUI infoText; // false일 때 출력할 안내 텍스트


    void Start()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);

        if (infoText != null)
            infoText.gameObject.SetActive(false); // ✅ 시작 시 비활성화

        LockCursor(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puzzle"))
        {
            if (uiPanel != null)
                uiPanel.SetActive(true);

            if (playerMovementScript != null)
                playerMovementScript.enabled = false;

            if (cameraScript != null)
                cameraScript.enabled = false;

            LockCursor(false);

            // ✅ 점수 시작은 한 번만
            if (!hasStartedScoring && scoreManager != null)
            {
                scoreManager.StartScoring();
                hasStartedScoring = true;
            }
        }
    }

    void LockCursor(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

    public void ClosePuzzleUI()
    {
        if (!isScorable)
        {
            if (infoText != null)
            {
                infoText.text = "False";
                infoText.gameObject.SetActive(true); // ✅ 텍스트 표시
                StartCoroutine(HideInfoTextAfterDelay(3f)); // ✅ 3초 후 숨김
            }

            return; // UI 닫지 않음
        }

        // ✅ 점수화 퍼즐일 경우 → 기존 로직 유지
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
        {
            infoText.gameObject.SetActive(false);
        }
    }
}
