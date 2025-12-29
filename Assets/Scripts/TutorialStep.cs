using UnityEngine;

[System.Serializable]
public class TutorialStep
{
    [TextArea(3, 10)]
    public string message;
    
    public enum AdvanceMode { Time, Input, Manual }
    public AdvanceMode advanceMode;

    [Header("Settings (Depending on Mode)")]
    public float timeDuration = 3f;
    public KeyCode triggerKey = KeyCode.Space;
}