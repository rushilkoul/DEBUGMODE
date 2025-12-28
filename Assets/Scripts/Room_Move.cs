using UnityEngine;

public class Room_Move : MonoBehaviour
{
    public float scrollSpeedY = 0.5f;
    
    private Material material;
    private Vector2 offset;
    
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        
        if (renderer != null)
        {
            material = renderer.material;
            offset = material.mainTextureOffset;
        }
    }
    
    void Update()
    {
        if (material != null)
        {
            offset.y += scrollSpeedY * Time.deltaTime;
            material.mainTextureOffset = offset;
        }
    }
}
