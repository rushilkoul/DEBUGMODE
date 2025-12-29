using UnityEngine;

public class StackOverflowBox : MonoBehaviour
{
    public float detectionRange = 2f;
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
    
    Renderer rend = GetComponent<Renderer>();
    if (rend != null)
    {
        rend.material.color = Color.green;
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

}