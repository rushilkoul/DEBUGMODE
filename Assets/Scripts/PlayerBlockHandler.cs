using UnityEngine;

public class PlayerBlockHandler : MonoBehaviour
{
  [Header("Pickup")]
  public float pickupRange = 3f;
  public Transform holdPoint;

  [Header("Grid")]
  public Manager grid;

  [Header("Placement")]
  public LayerMask placementMask;

  [Header("Held Block Visual")]
  public Vector3 heldScale = Vector3.one * 0.6f;

  GameObject heldBlock;
  Quaternion storedRotation;
  Vector3 originalScale;
  Collider heldCollider;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.E))
    {
      if (heldBlock == null)
        TryPickup();
      else
        TryDrop();
    }

    if (heldBlock != null)
    {
      // rotate left
      if (Input.GetKeyDown(KeyCode.Q))
        RotateHeld(-90f);

      // rotate right
      if (Input.GetKeyDown(KeyCode.R))
        RotateHeld(90f);
    }
  }

  void RotateHeld(float angle)
  {
    storedRotation *= Quaternion.Euler(0f, angle, 0f);
    heldBlock.transform.rotation = storedRotation;
  }

  void TryPickup()
  {
    Ray ray = new Ray(transform.position, transform.forward);

    if (!Physics.Raycast(ray, out RaycastHit hit, pickupRange))
      return;

    Transform root = hit.collider.transform.root;

    if (!root.CompareTag("Pickable Block"))
      return;

    heldBlock = root.gameObject;

    // cache transform data
    storedRotation = heldBlock.transform.rotation;
    originalScale = heldBlock.transform.localScale;

    // disable collider so it doesn't block placement raycasts
    heldCollider = heldBlock.GetComponentInChildren<Collider>();
    if (heldCollider != null)
      heldCollider.enabled = false;

    // attach to hold point but keep world rotation
    heldBlock.transform.SetParent(holdPoint, true);
    heldBlock.transform.position = holdPoint.position;
    heldBlock.transform.rotation = storedRotation;
    heldBlock.transform.localScale = heldScale;
  }

  void TryDrop()
  {
    Ray ray = new Ray(transform.position, transform.forward);

    // must hit ground or another block
    if (!Physics.Raycast(ray, out RaycastHit hit, pickupRange, placementMask))
      return;


    Vector3 placePoint = hit.point + hit.normal * (grid.cellSize * 0.5f);
    if (!grid.IsInsideGrid(placePoint))
      return;

    Vector3 snappedPos = grid.SnapToGrid(placePoint);
    GameObject placedBlock = heldBlock;

    heldBlock.transform.SetParent(null, true);
    heldBlock.transform.position = snappedPos;
    heldBlock.transform.rotation = storedRotation;
    heldBlock.transform.localScale = originalScale;

    // re-enable collider
    if (heldCollider != null)
      heldCollider.enabled = true;

    heldBlock = null;
    heldCollider = null;
    Collider[] nearby = Physics.OverlapSphere(placedBlock.transform.position, 5f);
    foreach (Collider col in nearby)
    {
        StackOverflowBox overflow = col.GetComponent<StackOverflowBox>();
        if (overflow != null)
        {
            overflow.OnBoxPlacedNearby();
        }
    }
  }

}
