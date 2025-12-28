using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
  public int width = 10;
  public int height = 5;
  public int depth = 10;
  public float cellSize = 1.7778f;

  public Dictionary<Vector3Int, BlockLogic> blockMap = new();


  public Vector3 SnapToGrid(Vector3 worldPos)
  {
    int x = Mathf.RoundToInt(worldPos.x / cellSize);
    int y = Mathf.RoundToInt(worldPos.y / cellSize);
    int z = Mathf.RoundToInt(worldPos.z / cellSize);

    return new Vector3(
        x * cellSize,
        y * cellSize,
        z * cellSize
    );
  }

  void OnDrawGizmos()
  {
    Color fillColor = new Color(0f, 1f, 1f, 0.04f);
    Color wireColor = new Color(0f, 1f, 1f, 0.18f);

    Camera cam = Camera.current;
    Vector3 camPos = cam != null ? cam.transform.position : Vector3.zero;

    for (int x = 0; x < width; x++)
    {
      for (int y = 0; y < height; y++)
      {
        for (int z = 0; z < depth; z++)
        {
          Vector3 pos = new Vector3(
              x * cellSize,
              y * cellSize,
              z * cellSize
          );

          float dist = Vector3.Distance(camPos, pos);
          float fade = Mathf.Clamp01(1f - dist / 12f);
          Gizmos.color = new Color(
              fillColor.r,
              fillColor.g,
              fillColor.b,
              fillColor.a * fade
          );
          Gizmos.DrawCube(pos, Vector3.one * cellSize);
          Gizmos.color = new Color(
              wireColor.r,
              wireColor.g,
              wireColor.b,
              wireColor.a * fade
          );
          Gizmos.DrawWireCube(pos, Vector3.one * cellSize);
        }
      }
    }
  }
  public bool IsInsideGrid(Vector3 worldPos)
  {
    int x = Mathf.RoundToInt(worldPos.x / cellSize);
    int y = Mathf.RoundToInt(worldPos.y / cellSize);
    int z = Mathf.RoundToInt(worldPos.z / cellSize);

    return x >= 0 && x < width &&
           y >= 0 && y < height &&
           z >= 0 && z < depth;
  }
  public void ValidatePipes()
  {
    // clear all bugs first
    foreach (var b in blockMap.Values)
      b.SetBugged(false);

    // find source
    Vector3Int sourcePos = Vector3Int.zero;
    bool found = false;

    foreach (var kv in blockMap)
    {
      if (kv.Value.isSource)
      {
        sourcePos = kv.Key;
        found = true;
        break;
      }
    }

    if (!found)
    {
      Debug.LogWarning("No SOURCE block found");
      return;
    }

    HashSet<Vector3Int> visited = new();
    Queue<Vector3Int> q = new();

    q.Enqueue(sourcePos);
    visited.Add(sourcePos);

    while (q.Count > 0)
    {
      Vector3Int current = q.Dequeue();
      BlockLogic pipe = blockMap[current];

      foreach (PipeDir dir in System.Enum.GetValues(typeof(PipeDir)))
      {
        if (dir == PipeDir.None) continue;
        if (!pipe.connections.HasFlag(dir)) continue;

        Vector3Int nextPos = current + DirToOffset(dir);

        if (!blockMap.ContainsKey(nextPos))
          continue;

        BlockLogic next = blockMap[nextPos];

        if (!next.connections.HasFlag(Opposite(dir)))
          continue;

        if (visited.Contains(nextPos))
          continue;

        visited.Add(nextPos);
        q.Enqueue(nextPos);
      }
    }

    // mark bugged pipes
    foreach (var kv in blockMap)
    {
      if (!visited.Contains(kv.Key))
        kv.Value.SetBugged(true);
    }
  }

  // helpers
  Vector3Int DirToOffset(PipeDir dir)
  {
    return dir switch
    {
      PipeDir.North => new Vector3Int(0, 0, 1),
      PipeDir.South => new Vector3Int(0, 0, -1),
      PipeDir.East => new Vector3Int(1, 0, 0),
      PipeDir.West => new Vector3Int(-1, 0, 0),
      _ => Vector3Int.zero
    };
  }

  PipeDir Opposite(PipeDir dir)
  {
    return dir switch
    {
      PipeDir.North => PipeDir.South,
      PipeDir.South => PipeDir.North,
      PipeDir.East => PipeDir.West,
      PipeDir.West => PipeDir.East,
      _ => PipeDir.None
    };
  }



}
