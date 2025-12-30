using UnityEngine;

public class BlockLogic : MonoBehaviour
{
  [Header("Pipe")]
  public PipeDir connections;

  [Header("State")]
  public bool isSource;
  public bool isDrain;
  public bool hasFluid;
  public bool isBugged;

  [Header("Bug Visual")]
  public GameObject blockVisual;
  public GameObject[] pipes;
  public Material normalMaterial;
  public Material buggedMaterial;
  public Material notPickableMaterial;
  public GameObject buggedTextParent;
  public DrainCompleteLogic drainCompleteLogic;

  [Header("External Managers")]
  public WallUnlock unlocker;


  void Awake()
  {
    if (transform.CompareTag("Pickable Block"))
    {
      Renderer rend = blockVisual.GetComponent<Renderer>();
      rend.material = notPickableMaterial;
    }
  }
  public void SetSource(bool value)
  {
    isSource = value;
  }
  public void SetFluid(bool value)
  {
    hasFluid = value;
    isBugged = !value && !isSource;

    if (isDrain && hasFluid)
    {

      drainCompleteLogic.OnComplete();
      if (unlocker != null)
      {
        unlocker.DisableWall();
      }
    }

    if (buggedTextParent != null)
      buggedTextParent.SetActive(isBugged);

    if (pipes != null)
    {
      foreach (var p in pipes)
      {
        var rend = p.GetComponent<Renderer>();
        if (rend != null)
          rend.material = isBugged ? buggedMaterial : normalMaterial;
      }
    }
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
