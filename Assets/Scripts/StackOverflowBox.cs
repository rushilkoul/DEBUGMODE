using UnityEngine;

public class StackOverflowBox : MonoBehaviour
{
    public float detectionRange = 2f;
    public string fixBoxTag = "Pickable Block";
    public ParticleSystem overflowParticles;
    
    private bool isFixed = false;
    private float detectionCooldown = 0f;
    public GameObject wallToCollapse;
    
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
        Collider[] nearbyBoxes = Physics.OverlapSphere(transform.position, detectionRange);
        
        foreach (Collider col in nearbyBoxes)
        {
            Transform root = col.transform.root;
            if (root.CompareTag(fixBoxTag) && root.gameObject != gameObject)
            {
                FixOverflow(root.gameObject);
                break;
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
    
    Destroy(fixBox, 0.5f);
    
    Renderer rend = GetComponent<Renderer>();
    if (rend != null)
    {
        rend.material.color = Color.green;
    }
    
    if (wallToCollapse != null)
    {
        CollapseWall();
    }
}
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
    public void OnBoxPlacedNearby()
    {
        detectionCooldown = 0.5f;
    }
    void CollapseWall()
{
    
    Rigidbody rb = wallToCollapse.GetComponent<Rigidbody>();
    if (rb == null)
    {
        rb = wallToCollapse.AddComponent<Rigidbody>();
    }
    
    rb.isKinematic = false; 
    rb.useGravity = true;   
    
    
    rb.AddForce(Vector3.forward * 2f, ForceMode.Impulse);
    
    
    Destroy(wallToCollapse, 3f); 
    
    Debug.Log("Wall collapsed!");
}
}