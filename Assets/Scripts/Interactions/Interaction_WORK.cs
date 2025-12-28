using UnityEngine;

public class Interaction_WORK : InteractionBehaviour
{
    [SerializeField] GameObject viewCam;
    [SerializeField] GameObject leavePanel;
    [SerializeField] GameObject screenSpaceCanvas;
    public override void OnInput()
    {
        Cursor.lockState = CursorLockMode.None; 
        viewCam.SetActive(true);
        StartCoroutine(AnimateFOV(viewCam.GetComponent<Camera>(), 60f, 45f, 0.2f));
        leavePanel.SetActive(true);
        screenSpaceCanvas.SetActive(false);
    }
    public override void OnDoubleInput()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        viewCam.SetActive(false);
        leavePanel.SetActive(false);
        screenSpaceCanvas.SetActive(true);
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