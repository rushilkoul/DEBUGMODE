using UnityEngine;

public class Interaction_WORK : InteractionBehaviour
{
    [SerializeField] GameObject viewCam;
    public override void OnInput()
    {
        viewCam.SetActive(true);
    }
    public override void OnDoubleInput()
    {
        viewCam.SetActive(false);
    }
}