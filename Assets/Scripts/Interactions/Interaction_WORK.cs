using UnityEngine;

public class Interaction_WORK : InteractionBehaviour
{
    [SerializeField] GameObject viewCam;
    [SerializeField] GameObject leavePanel;
    [SerializeField] GameObject screenSpaceCanvas;
    [SerializeField] GameObject lockScreen;
    [SerializeField] GameObject player;
    public override void OnInput()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
        viewCam.SetActive(true);
        StartCoroutine(AnimateFOV(viewCam.GetComponent<Camera>(), 60f, 45f, 0.2f));
        leavePanel.SetActive(true);
        screenSpaceCanvas.SetActive(false);
        player.GetComponentInChildren<CameraView>().enabled = false;
        player.GetComponentInChildren<Movement>().enabled = false;
        lockScreen.SetActive(false);
    }
    public override void OnDoubleInput()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
        viewCam.SetActive(false);
        leavePanel.SetActive(false);
        screenSpaceCanvas.SetActive(true);
        player.GetComponentInChildren<CameraView>().enabled = true;
        player.GetComponentInChildren<Movement>().enabled = true;
        lockScreen.SetActive(true);
    }
    private System.Collections.IEnumerator AnimateFOV(Camera cam, float fromFOV, float toFOV, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            cam.fieldOfView = Mathf.Lerp(fromFOV, toFOV, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cam.fieldOfView = toFOV;
    }
}