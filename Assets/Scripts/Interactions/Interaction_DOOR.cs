using UnityEngine;

public class Interaction_DOOR : InteractionBehaviour
{
  [SerializeField] private Transform player;
  [SerializeField] private Vector3 pos;
  [SerializeField] private Quaternion rotation;
  public override void OnInput()
  {
    player.position = pos;
    player.rotation = rotation;
  }
  public override void OnDoubleInput()
  {
    Debug.Log("door twice");
  }
}
