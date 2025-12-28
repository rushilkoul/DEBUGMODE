using UnityEngine;

public class HiddenPlatform : MonoBehaviour
{
    public float visibleDuration = 5f;
    private Renderer platformRenderer;
    private Collider platformCollider;
    private bool isVisible = false;
    private float visibleTimer = 0f;
    
    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider>();
        MakeInvisible();
    }
    
    void Update()
    {
        
        if (isVisible)
        {
            visibleTimer -= Time.deltaTime;
            
            if (visibleTimer <= 0f)
            {
                MakeInvisible();
                isVisible = false;
            }
        }
    }
    
    public void Reveal()
    {
        isVisible = true;
        visibleTimer = visibleDuration;
        
        if (platformRenderer != null)
        {
            platformRenderer.enabled = true;
        }
        
        if (platformCollider != null)
        {
            platformCollider.enabled = true;
        }
    }
    
    void MakeInvisible()
    {
        if (platformRenderer != null)
        {
            platformRenderer.enabled = false;
        }
    }
}