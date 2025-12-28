using UnityEngine;

public class Interaction_SETTINGS : InteractionBehaviour
{
    [SerializeField] GameObject viewCam;
    [SerializeField] GameObject leavePanel;
    [SerializeField] GameObject SettingsGFX;
    [SerializeField] GameObject screenSpaceCanvas;
    [SerializeField] GameObject player;
    public override void OnInput()
    {
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        viewCam.SetActive(true);
        leavePanel.SetActive(true);
        SettingsGFX.SetActive(false);
        player.GetComponentInChildren<CameraView>().enabled = false;
        player.GetComponentInChildren<Movement>().enabled = false;
        screenSpaceCanvas.SetActive(false);
    }
    public override void OnDoubleInput()
    {
        leavePanel.GetComponent<Settings>().BackButton();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
        viewCam.SetActive(false);
        leavePanel.SetActive(false);
        SettingsGFX.SetActive(true);
        player.GetComponentInChildren<CameraView>().enabled = true;
        player.GetComponentInChildren<Movement>().enabled = true;
        screenSpaceCanvas.SetActive(true);
    }
}