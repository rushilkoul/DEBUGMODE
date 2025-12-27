using UnityEngine;
using UnityEngine.Rendering;

public class CameraView : MonoBehaviour
{
  [Header("Camera Viewing")]
  public Transform player;
  public float sens;
  float xRotation = 0f;

  [Header("Effect References")]
  public Volume overrideVolume;
  public Camera playerCamera;
  public AudioSource audioSource;
  public AudioClip effectSound;

  [Header("Settings")]
  public float zoomFOV = 40f;
  public float smoothSpeed = 10f;

  private float defaultFOV;
  private float targetWeight;


  private GameManager GameManagerInstance;
  void Start()
  {
    defaultFOV = playerCamera.fieldOfView;
    GameManagerInstance = GameManager.Instance;
    

    if (overrideVolume != null) overrideVolume.weight = 0f;
    Cursor.lockState = CursorLockMode.Locked;
  }

  void Update()
  {
    if(GameManagerInstance != null)
    {    
        bool isHoldingRightClick = Input.GetMouseButton(1);

        if (Input.GetMouseButtonDown(1))
        {
        if (audioSource != null && effectSound != null)
        {
            audioSource.PlayOneShot(effectSound);
        }
        }

        if (isHoldingRightClick)
        {
        GameManagerInstance.setDebugMode(true);
        targetWeight = 1f;

        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFOV, Time.deltaTime * smoothSpeed);
        }
        else
        {
        GameManagerInstance.setDebugMode(false);
        targetWeight = 0f;

        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, defaultFOV, Time.deltaTime * smoothSpeed);
        }
        if (overrideVolume != null)
        {
        overrideVolume.weight = Mathf.Lerp(overrideVolume.weight, targetWeight, Time.deltaTime * smoothSpeed);
        }
    }


    float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
    float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    player.Rotate(Vector3.up * mouseX);


  }
}