using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public Transform player;
    public float Speed= 10f;
    public float jforce= 10f;
    public LayerMask groundLayer;
    public float rayDistance;
    private bool isGrounded;
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayer);
        Color debugColor = isGrounded ? Color.green : Color.red;
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, debugColor);
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
       Move(); 
    }
    void Move()
    {
        Vector3 dir= Vector3.zero;
        if(Input.GetKey(KeyCode.W)) dir += player.forward;
        if(Input.GetKey(KeyCode.S)) dir -= player.forward;
        if(Input.GetKey(KeyCode.A)) dir -= player.right;
        if(Input.GetKey(KeyCode.D)) dir += player.right;

        rb.AddForce(dir* Speed, ForceMode.Force);
    }
    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jforce, ForceMode.Impulse);
    }
}
