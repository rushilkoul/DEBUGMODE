using UnityEngine;

public class Interaction_EXIT : InteractionBehaviour
{
    public override void OnInput()
    {
        Debug.Log("exit the game");
    }
    public override void OnDoubleInput()
    {
        // viewCam.SetActive(false);
    }
}