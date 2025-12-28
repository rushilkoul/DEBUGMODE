using UnityEngine;

public class Interaction_SETTINGS : InteractionBehaviour
{
    [SerializeField] GameObject viewCam;
    [SerializeField] GameObject leavePanel;
    [SerializeField] GameObject SettingsGFX;
    public override void OnInput()
    {
        Cursor.lockState = CursorLockMode.None; 
        viewCam.SetActive(true);
        leavePanel.SetActive(true);
        SettingsGFX.SetActive(false);
    }
    public override void OnDoubleInput()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        viewCam.SetActive(false);
        leavePanel.SetActive(false);
        SettingsGFX.SetActive(true);
    }
}