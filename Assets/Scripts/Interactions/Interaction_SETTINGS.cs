using UnityEngine;

public class Interaction_SETTINGS : InteractionBehaviour
{
    public override void OnInput()
    {
        Debug.Log("OPEN SETTINGS");
    }
    public override void OnDoubleInput()
    {
        // viewCam.SetActive(false);
    }
}