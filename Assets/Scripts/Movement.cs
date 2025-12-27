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
    public float drag= 0.3f;
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
        float horizontal = Input.GetAxisRaw("Horizontal"); 
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = (player.forward * vertical) + (player.right * horizontal);

        float cForce = isGrounded ? Speed : Speed * drag;
        rb.AddForce(dir* cForce* Speed, ForceMode.Force);
    }
    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jforce, ForceMode.Impulse);
    }
}
