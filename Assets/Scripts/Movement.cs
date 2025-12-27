using UnityEngine;

public class Movement : MonoBehaviour
{
  [Header("References")]
  public Rigidbody rb;
  public Transform orientation;

  [Header("Movement Stats")]
  public float moveSpeed = 4f;
  public float jumpForce = 5f;
  public float groundDrag = 6f;
  public float airMultiplier = 0.4f;

  [Header("Ground Detection")]
  public LayerMask groundLayer;
  public float rayDistance = 1.1f;
  public bool isGrounded;

  float horizontalInput;
  float verticalInput;
  Vector3 moveDirection;


  void Start()
  {
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

    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    {
      Jump();
    }
  }

  void MovePlayer()
  {
    moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
    if (isGrounded)
    {
      rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
    else
    {
      rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }
  }

  void Jump()
  {
    Vector3 vel = rb.linearVelocity;
    rb.linearVelocity = new Vector3(vel.x, 0f, vel.z);

    rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
  }
}