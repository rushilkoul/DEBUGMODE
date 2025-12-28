using UnityEngine;

public class Room_Move : MonoBehaviour
{
    public float speed = 0.5f;
    public float idlespeed = 0.1f;
    private Transform player;
    private Vector3 lastpos;
    
    private Material material;
    private Vector2 offset;
    private Vector2 lastDirection; 
    
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.transform;
        lastpos = player.position;
        lastDirection = Vector2.zero;
        
        Renderer renderer = GetComponent<Renderer>();
        
        if (renderer != null)
        {
            material = renderer.material;
            offset = material.mainTextureOffset;
        }
    }
    
    void Update()
    {
        if (material != null && player != null)
        {
            Vector3 movement = player.position - lastpos;
            
            if (movement.magnitude > 0.001f) 
            {
                offset.x += speed * movement.x;
                offset.y += speed * movement.z; 
                
                lastDirection = new Vector2(movement.x, movement.z).normalized;
            }
            else
            {
                if (lastDirection != Vector2.zero)
                {
                    offset.x += idlespeed * lastDirection.x * Time.deltaTime;
                    offset.y += idlespeed * lastDirection.y * Time.deltaTime;
                }
            }
            
            material.mainTextureOffset = -offset;
            lastpos = player.position;
        }
    }
}
