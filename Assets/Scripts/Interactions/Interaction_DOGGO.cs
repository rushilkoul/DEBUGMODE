using UnityEngine;

public class Interaction_DOGGO : InteractionBehaviour
{
    [SerializeField] AudioSource bark;
    public override void OnInput()
    {
        bark.Play();
    }
    public override void OnDoubleInput()
    {
        bark.Play();
    }
}
