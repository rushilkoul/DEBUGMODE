using UnityEngine;

public class BlockLogic : MonoBehaviour
{
  [Header("Pipe")]
  public PipeDir connections;

  [Header("State")]
  public bool isSource;
  public bool hasFluid;
  public bool isBugged;

  [Header("Bug Visual")]
  public GameObject bug;

  public void SetFluid(bool value)
  {
    hasFluid = value;
    isBugged = !value && !isSource;

    if (bug != null)
      bug.SetActive(isBugged);
  }

  public void RotateLeft()
  {
    connections = Rotate(connections, true);
  }

  public void RotateRight()
  {
    connections = Rotate(connections, false);
  }

  PipeDir Rotate(PipeDir d, bool left)
  {
    PipeDir r = PipeDir.None;

    if (left)
    {
      if (d.HasFlag(PipeDir.North)) r |= PipeDir.West;
      if (d.HasFlag(PipeDir.West)) r |= PipeDir.South;
      if (d.HasFlag(PipeDir.South)) r |= PipeDir.East;
      if (d.HasFlag(PipeDir.East)) r |= PipeDir.North;
    }
    else
    {
      if (d.HasFlag(PipeDir.North)) r |= PipeDir.East;
      if (d.HasFlag(PipeDir.East)) r |= PipeDir.South;
      if (d.HasFlag(PipeDir.South)) r |= PipeDir.West;
      if (d.HasFlag(PipeDir.West)) r |= PipeDir.North;
    }

    return r;
  }
}
