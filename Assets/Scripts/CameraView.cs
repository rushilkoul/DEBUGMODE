using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

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

  [Header("UI Value Displays")]
  public TextMeshProUGUI sensValueText;
  public TextMeshProUGUI resScaleValueText; 
  public TextMeshProUGUI volValueText;

  [Header("UI Controls")]
 public UnityEngine.UI.Slider sensSlider;
 public UnityEngine.UI.Slider resSlider;
 public UnityEngine.UI.Slider volSlider;

 [Header("Graphics UI")]
 public TMP_Dropdown viewModeDropdown;
 public UnityEngine.UI.Slider resScaleSlider;


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
  public void UpdateSensPreview(float val)
  {
    if (sensValueText != null) sensValueText.text = val.ToString("0");
  }

  public void UpdateVolPreview(float val)
  {
    if (volValueText != null) 
        volValueText.text = (val * 100).ToString("0") + "%";
  }

  public void UpdateResPreview(float val)
  {
    if (resScaleValueText != null) 
        resScaleValueText.text = (val * 100).ToString("0") + "%";
  }
  public void SaveSettings()
  {
    sens = sensSlider.value;
    if (audioSource != null) audioSource.volume = volSlider.value;
    
    QualitySettings.resolutionScalingFixedDPIFactor = resScaleSlider.value;
    if (viewModeDropdown.value == 0)
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }
    else
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    PlayerPrefs.SetFloat("Sensitivity", sens);
    PlayerPrefs.SetFloat("Volume", volSlider.value);
    PlayerPrefs.SetInt("ViewMode", viewModeDropdown.value);
    PlayerPrefs.SetFloat("ResScale", resScaleSlider.value);
    PlayerPrefs.Save();

    Debug.Log("Settings Applied and Saved!");
  }

  public void BackButton()
  {
    sensSlider.value = sens;
    if (audioSource != null) volSlider.value = audioSource.volume;
    
    viewModeDropdown.value = (Screen.fullScreenMode == FullScreenMode.Windowed) ? 1 : 0;
    
    resScaleSlider.value = QualitySettings.resolutionScalingFixedDPIFactor;

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }
}