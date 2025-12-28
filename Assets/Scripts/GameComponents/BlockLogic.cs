using UnityEngine;

[System.Flags]
public enum PipeDir
{
  None = 0,
  North = 1, // +Z
  East = 2, // +X
  South = 4, // -Z
  West = 8  // -X
}

public class BlockLogic : MonoBehaviour
{
  [Header("Pipe Connections")]
  public PipeDir connections;

  [Header("Flags")]
  public bool isSource;
  public bool isBugged;

  public void SetBugged(bool bugged)
  {
    isBugged = bugged;

    // TEMP visual feedback
    var r = GetComponentInChildren<Renderer>();
    if (r != null)
      r.material.SetColor("_EmissionColor",
          bugged ? Color.red : Color.cyan);
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
