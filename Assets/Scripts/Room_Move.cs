using UnityEngine;

public class Room_Move : MonoBehaviour
{
    [Header("Speed Settings")]
    [Tooltip("Multiplier for how fast the room moves compared to the player. 1.0 = Sync, 1.5 = Room moves 50% faster.")]
    public float moveSpeedMultiplier = 1.2f; 

    [Tooltip("How fast the room keeps flowing when the player stops moving.")]
    public float idleFlowSpeed = 0.5f;

    [Header("Shader References")]
    public string propertyName = "_ScrollOffset";

    private Transform player;
    private Vector3 lastPos;
    private Material material;
    
    private Vector2 currentOffset;
    private Vector2 lastMoveDirection; 

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            lastPos = player.position;
        }

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;

            if (material.HasProperty(propertyName))
            {
                Vector4 vec = material.GetVector(propertyName);
                currentOffset = new Vector2(vec.x, vec.y);
            }
        }

        lastMoveDirection = Vector2.up; 
    }

    void Update()
    {
        if (material == null || player == null) return;

        Vector3 playerDelta = player.position - lastPos;
        Vector2 textureDelta = new Vector2(playerDelta.x, playerDelta.z);

        if (textureDelta.magnitude > 0.001f)
        {
            currentOffset += textureDelta * moveSpeedMultiplier;
            lastMoveDirection = textureDelta.normalized;
        }
        else
        {
            currentOffset += lastMoveDirection * idleFlowSpeed * Time.deltaTime;
        }

        material.SetVector(propertyName, -currentOffset);
        lastPos = player.position;
    }
}