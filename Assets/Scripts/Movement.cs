using UnityEngine;

public class Movement : MonoBehaviour
{
  [Header("References")]
  public Rigidbody rb;
  public Transform orientation;

  [Header("Movement Stats")]
  public float moveSpeed = 6f;
  public float sprintSpeed = 10f;
  public float jumpForce = 12f;
  public float groundDrag = 6f;
  public float airMultiplier = 0.4f;
  public float GravityScale= 5f;

  [Header("Ground Detection")]
  public LayerMask groundLayer;
  public float rayDistance = 1.1f;
  public bool isGrounded;

  float horizontalInput;
  float verticalInput;
  Vector3 moveDirection;

  private float currentSpeed;
  public bool isSprinting;


  void Start()
  {
    currentSpeed= moveSpeed;
    if (rb == null) rb = GetComponent<Rigidbody>();
    rb.freezeRotation = true;
  }

  void Update()
  {
    isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayer);

    Debug.DrawRay(transform.position, Vector3.down * rayDistance, isGrounded ? Color.green : Color.red);

    if (isGrounded)
      rb.linearDamping = groundDrag;
    else
      rb.linearDamping = 0;
    
    if (!isGrounded)
    {
        rb.AddForce(Vector3.down * GravityScale, ForceMode.Force);
    }

    GetInput();
  }

  void FixedUpdate()
  {
    MovePlayer();
  }

  void GetInput()
  {
    horizontalInput = Input.GetAxisRaw("Horizontal");
    verticalInput = Input.GetAxisRaw("Vertical");

    if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
    {
        currentSpeed = sprintSpeed;
        isSprinting = true;
    }
    else
    {
        currentSpeed = moveSpeed;
        isSprinting = false;
    }

    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    {
      Jump();
    }
  }

  void MovePlayer()
{
    moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
    
    if (moveDirection.magnitude > 0.1f)
    {
        Vector3 targetVelocity = moveDirection.normalized * currentSpeed;
        
        if (isGrounded)
        {
            rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
        }
        else
        {
            Vector3 currentVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            Vector3 newVel = Vector3.Lerp(currentVel, targetVelocity, airMultiplier);
            rb.linearVelocity = new Vector3(newVel.x, rb.linearVelocity.y, newVel.z);
        }
    }
    else if (isGrounded)
    {
        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
    }
}

  void Jump()
  {
    Vector3 vel = rb.linearVelocity;
    rb.linearVelocity = new Vector3(vel.x, 0f, vel.z);

    rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
  }

  
}