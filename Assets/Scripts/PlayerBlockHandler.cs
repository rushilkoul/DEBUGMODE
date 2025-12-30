using UnityEngine;

public class PlayerBlockHandler : MonoBehaviour
{
  [Header("References")]
  private Transform playerCamera;

  [Header("Pickup")]
  public float pickupRange = 3f;
  public Transform holdPoint;

  [Header("Grid")]
  public GridManager grid;

  [Header("Placement")]
  public LayerMask placementMask;

  [Header("Held Block Visual")]
  public Vector3 heldScale = Vector3.one * 0.6f;

  GameObject heldBlock;
  Quaternion storedRotation;
  Vector3 originalScale;
  Collider heldCollider;
  Vector3Int lastCell;

  void Start()
  {
    playerCamera = transform;

    grid.ValidateFluid();
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.E))
    {
      if (heldBlock == null)
        TryPickup();
      else
        TryDrop();
      grid.ValidateFluid();
    }

    if (heldBlock != null)
    {
      if (Input.GetKeyDown(KeyCode.Q))
        RotateHeld(-90f);

      if (Input.GetKeyDown(KeyCode.R))
        RotateHeld(90f);
    }
  }

  void RotateHeld(float angle)
  {
    storedRotation *= Quaternion.Euler(0f, angle, 0f);
    heldBlock.transform.rotation = storedRotation;

    BlockLogic logic = heldBlock.GetComponent<BlockLogic>();
    if (logic != null)
    {
      if (angle < 0) logic.RotateLeft();
      else logic.RotateRight();
    }
  }

  void TryPickup()
  {
    Ray ray = new(playerCamera.position, playerCamera.forward);

    if (!Physics.Raycast(ray, out RaycastHit hit, pickupRange))
    {
      Debug.Log(hit.collider.gameObject);
      return;
    }

    Transform root = hit.collider.transform.parent;

    if (root == null || !(root.CompareTag("Pickable Block") || root.CompareTag("StackBlock")))
      return;

    heldBlock = root.gameObject;

    lastCell = new Vector3Int(
        Mathf.RoundToInt(heldBlock.transform.position.x / grid.cellSize),
        Mathf.RoundToInt(heldBlock.transform.position.y / grid.cellSize),
        Mathf.RoundToInt(heldBlock.transform.position.z / grid.cellSize)
    );

    if (grid.blockMap.ContainsKey(lastCell))
      grid.blockMap.Remove(lastCell);

    storedRotation = heldBlock.transform.rotation;
    originalScale = heldBlock.transform.localScale;

    heldCollider = heldBlock.GetComponentInChildren<Collider>();
    if (heldCollider != null)
      heldCollider.enabled = false;

    heldBlock.transform.SetParent(holdPoint, true);
    heldBlock.transform.position = holdPoint.position;
    heldBlock.transform.localScale = heldScale;

    grid.ValidateFluid();
  }

  void TryDrop()
  {
    Ray ray = new(playerCamera.position, playerCamera.forward);

    if (!Physics.Raycast(ray, out RaycastHit hit, pickupRange, placementMask, QueryTriggerInteraction.Ignore))
    {
      Debug.Log(hit.collider.gameObject);
      return;
    }

    Vector3 placePoint = hit.point + hit.normal * (grid.cellSize * 0.5f);
    if (!grid.IsInsideGrid(placePoint))
    {
      Debug.Log(placePoint);
      return;
    }

    Vector3 snappedPos = grid.SnapToGrid(placePoint);

    heldBlock.transform.SetParent(null, true);
    heldBlock.transform.position = snappedPos;
    heldBlock.transform.rotation = storedRotation;
    heldBlock.transform.localScale = originalScale;

    if (heldCollider != null)
      heldCollider.enabled = true;

    Vector3Int cell = new(
        Mathf.RoundToInt(snappedPos.x / grid.cellSize),
        Mathf.RoundToInt(snappedPos.y / grid.cellSize),
        Mathf.RoundToInt(snappedPos.z / grid.cellSize)
    );

    grid.blockMap[cell] = heldBlock.GetComponent<BlockLogic>();

    GameObject placedBlock = heldBlock;

    heldBlock = null;
    heldCollider = null;
    Collider[] nearby = Physics.OverlapSphere(placedBlock.transform.position, 5f);

    foreach (Collider col in nearby)
    {
      if (col == null) continue;

      StackOverflowBox overflow = col.GetComponent<StackOverflowBox>();
      if (overflow != null)
      {
        overflow.OnBoxPlacedNearby();
      }
    }
  }
}