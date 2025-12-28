using UnityEngine;

public class Interaction_EXIT : InteractionBehaviour
{
    public override void OnInput()
    {
        Application.Quit();
    }
    public override void OnDoubleInput()
    {
        // do nothing the game has been quit lmao
    }
}