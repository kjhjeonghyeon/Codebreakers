using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTriggerUI_KJS : MonoBehaviour
{
    public GameObject uiPanel;
    public PlayerMove_KJS playerMovementScript;
    public CameraMove cameraScript;

    void Start()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);

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

            LockCursor(false); // 커서 보이게
        }
    }

    void LockCursor(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

    public void ClosePuzzleUI()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);

        if (playerMovementScript != null)
            playerMovementScript.enabled = true;

        if (cameraScript != null)
            cameraScript.enabled = true;

        LockCursor(true); // 커서 다시 숨기고 잠금
    }

}
