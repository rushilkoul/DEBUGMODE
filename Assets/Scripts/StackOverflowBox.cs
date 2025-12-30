using UnityEngine;

public class StackOverflowBox : MonoBehaviour
{
        public string fixBoxTag = "Pickable Block";
    public ParticleSystem overflowParticles;
    
    private bool isFixed = false;
    private float detectionCooldown = 0f;
    
    void Start()
    {
        if (overflowParticles != null)
        {
            overflowParticles.Play();
        }
    }
    
    void Update()
    {
       if (detectionCooldown > 0)
        {
            detectionCooldown -= Time.deltaTime;
        }
        
        if (!isFixed && detectionCooldown <= 0) 
        {
            CheckForFixBox();
        }
    }
    
    void CheckForFixBox()
    {
        Vector3 rayStart = transform.position + Vector3.up * (1.7778f * 0.4f);
        Vector3 direction = Vector3.up;
        float distance = 1.7778f;

        RaycastHit hit;
        if (Physics.Raycast(rayStart, direction, out hit, distance))
        {
            Transform root = hit.collider.transform.root;

            if (root.CompareTag(fixBoxTag) && root.gameObject != gameObject)
            {
                FixOverflow(root.gameObject);
            }
        }
    }
    
    void FixOverflow(GameObject fixBox)
{
    isFixed = true;
    
    Debug.Log("Stack Overflow Fixed!");
    
    if (overflowParticles != null)
    {
        overflowParticles.Stop();
        overflowParticles.Clear();
    }
    
    Renderer rend = GetComponent<Renderer>();
    if (rend != null)
    {
        rend.material.color = Color.green;
    }
}
    
    void OnDrawGizmosSelected()
    {
    Vector3 rayStart = transform.position + Vector3.up * (1.7778f * 0.4f);
    float distance = 1.7778f;

    Gizmos.color = Color.red;
    Gizmos.DrawRay(rayStart, Vector3.up * distance);
    }
    public void OnBoxPlacedNearby()
    {
        detectionCooldown = 0.5f;
    }

}