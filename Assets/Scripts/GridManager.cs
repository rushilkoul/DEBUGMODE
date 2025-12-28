using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
  public int width = 10;
  public int height = 5;
  public int depth = 10;
  public float cellSize = 1.7778f;

  public Dictionary<Vector3Int, BlockLogic> blockMap = new();

  void Start()
  {
    BuildGridFromScene();
    ValidateFluid();
  }

  public Vector3 SnapToGrid(Vector3 worldPos)
  {
    return new Vector3(
        Mathf.RoundToInt(worldPos.x / cellSize) * cellSize,
        Mathf.RoundToInt(worldPos.y / cellSize) * cellSize,
        Mathf.RoundToInt(worldPos.z / cellSize) * cellSize
    );
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
  public void BuildGridFromScene()
  {
    blockMap.Clear();

    BlockLogic[] blocks = Object.FindObjectsByType<BlockLogic>(
        FindObjectsSortMode.None
    );

    foreach (var block in blocks)
    {
      Vector3 p = block.transform.position;
      Vector3Int cell = new(
          Mathf.RoundToInt(p.x / cellSize),
          Mathf.RoundToInt(p.y / cellSize),
          Mathf.RoundToInt(p.z / cellSize)
      );

      blockMap[cell] = block;
    }
  }

  public void ValidateFluid()
  {
    // reset all
    foreach (var b in blockMap.Values)
      b.SetFluid(false);

    Queue<Vector3Int> q = new();
    HashSet<Vector3Int> visited = new();

    // enqueue sources
    foreach (var kv in blockMap)
    {
      if (kv.Value.isSource)
      {
        kv.Value.SetFluid(true);
        q.Enqueue(kv.Key);
        visited.Add(kv.Key);
      }
    }

    if (q.Count == 0)
    {
      Debug.LogWarning("No SOURCE found");
      return;
    }

    // BFS
    while (q.Count > 0)
    {
      Vector3Int cur = q.Dequeue();
      BlockLogic curBlock = blockMap[cur];

      foreach (PipeDir dir in System.Enum.GetValues(typeof(PipeDir)))
      {
        if (dir == PipeDir.None) continue;
        if (!curBlock.connections.HasFlag(dir)) continue;

        Vector3Int next = cur + DirToOffset(dir);
        if (!blockMap.ContainsKey(next)) continue;
        if (visited.Contains(next)) continue;

        BlockLogic nextBlock = blockMap[next];
        if (!nextBlock.connections.HasFlag(Opposite(dir))) continue;

        visited.Add(next);
        nextBlock.SetFluid(true);
        q.Enqueue(next);
      }
    }
  }

  Vector3Int DirToOffset(PipeDir dir)
  {
    return dir switch
    {
      PipeDir.North => Vector3Int.forward,
      PipeDir.South => Vector3Int.back,
      PipeDir.East => Vector3Int.right,
      PipeDir.West => Vector3Int.left,
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
