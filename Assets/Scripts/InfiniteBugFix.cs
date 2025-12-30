using UnityEngine;

public class InfiniteBugFix : MonoBehaviour
{
    [Header("Objects to Disable")]
    public Collider cubePathCollider; 
    public GameObject crawlingCube;   

    public void OnPuzzleComplete()
    {
        if (cubePathCollider != null)
        {
            cubePathCollider.enabled = false;
            Debug.Log("Cube Path Collider Disabled!");
        }

        if (crawlingCube != null)
        {
            crawlingCube.SetActive(false);
            Debug.Log("Crawling Cube Disabled!");
        }
    }
}
