using UnityEngine;

public class WallUnlock : MonoBehaviour
{
    [Header("Target Wall")]
    public Collider wallCollider;

    public void DisableWall()
    {
        if (wallCollider != null)
        {
            wallCollider.enabled = false;
            Debug.Log("Wall Collider Disabled by WallUnlocker!");
        }
    }
}
