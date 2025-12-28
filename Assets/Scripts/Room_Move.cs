using UnityEngine;


public class Room_Move : MonoBehaviour
{
    public float speed = 0.5f;
    public Transform player;
    private Vector3 lastpos;
    
    private Material material;
    private Vector2 offset;
    
    void Start()
    {
        lastpos= player.position;
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
            Vector3 movement= player.position- lastpos;
            offset.x= speed*movement.x;
            offset.y += speed*movement.z;
            material.mainTextureOffset = -offset;
            lastpos= player.position;
        }
    }
}
