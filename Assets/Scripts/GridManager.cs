using UnityEngine;

public class GridManager : MonoBehaviour
{
  public int width = 10;
  public int height = 5;
  public int depth = 10;
  public float cellSize = 1.7778f;

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

}
