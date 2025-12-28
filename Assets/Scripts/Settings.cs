using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Game References")]
    public CameraView activeCameraView; 

    [Header("UI Value Displays")]
    public TextMeshProUGUI sensValueText;
    public TextMeshProUGUI volValueText;
    public TextMeshProUGUI resScaleValueText;

    [Header("UI Controls")]
    public Slider sensSlider;
    public Slider volSlider;
    public Slider resScaleSlider;
    public TMP_Dropdown viewModeDropdown;


    void OnEnable()
    {
        float savedSens = PlayerPrefs.GetFloat("Sensitivity", 1f);
        float savedVol = PlayerPrefs.GetFloat("Volume", 1f);
        float savedRes = PlayerPrefs.GetFloat("ResScale", 1f);
        int savedViewMode = PlayerPrefs.GetInt("ViewMode", 0);

        if (sensSlider != null) 
        {
            sensSlider.SetValueWithoutNotify(savedSens);
            if(sensValueText != null) sensValueText.text = savedSens.ToString("0.000");
        }

        if (volSlider != null) 
        {
            volSlider.SetValueWithoutNotify(savedVol);
            if(volValueText != null) volValueText.text = (savedVol * 100).ToString("0") + "%";
        }

        if (resScaleSlider != null)
        {
            resScaleSlider.SetValueWithoutNotify(savedRes);
            if(resScaleValueText != null) resScaleValueText.text = (savedRes * 100).ToString("0") + "%";
        }

        if (viewModeDropdown != null)
        {
            viewModeDropdown.SetValueWithoutNotify(savedViewMode);
        }

        if (sensSlider != null) sensSlider.onValueChanged.AddListener(SetSensitivity);
        if (volSlider != null) volSlider.onValueChanged.AddListener(SetVolume);
        if (resScaleSlider != null) resScaleSlider.onValueChanged.AddListener(SetResScale);
        if (viewModeDropdown != null) viewModeDropdown.onValueChanged.AddListener(SetViewMode);
    }

    void OnDisable()
    {
        if (sensSlider != null) sensSlider.onValueChanged.RemoveListener(SetSensitivity);
        if (volSlider != null) volSlider.onValueChanged.RemoveListener(SetVolume);
        if (resScaleSlider != null) resScaleSlider.onValueChanged.RemoveListener(SetResScale);
        if (viewModeDropdown != null) viewModeDropdown.onValueChanged.RemoveListener(SetViewMode);
        
        PlayerPrefs.Save();
    }

    public void SetSensitivity(float val)
    {
        if (activeCameraView != null) activeCameraView.sens = val;
        if (sensValueText != null) sensValueText.text = val.ToString("0.000");
        
        PlayerPrefs.SetFloat("Sensitivity", val);
        PlayerPrefs.Save(); 
    }

    public void SetVolume(float val)
    {
        AudioListener.volume = val;
        if (volValueText != null) volValueText.text = (val * 100).ToString("0") + "%";
        
        PlayerPrefs.SetFloat("Volume", val);
        PlayerPrefs.Save();
    }

    public void SetResScale(float val)
    {
        QualitySettings.resolutionScalingFixedDPIFactor = val;
        if (resScaleValueText != null) resScaleValueText.text = (val * 100).ToString("0") + "%";
        
        PlayerPrefs.SetFloat("ResScale", val);
        PlayerPrefs.Save();
    }

    public void SetViewMode(int index)
    {
        if (index == 0) 
        {
            Resolution maxRes = Screen.currentResolution;
            
            Screen.SetResolution(maxRes.width, maxRes.height, FullScreenMode.FullScreenWindow);
        }
        else 
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        PlayerPrefs.SetInt("ViewMode", index);
        PlayerPrefs.Save();
    }

    public void BackButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}