using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI tooltipText;

    [Header("Tutorial Steps")]
    public TutorialStep[] steps;
    
    public int currentIndex = 0;
    private Coroutine typingCoroutine;
    private float timer = 0f;
    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (steps.Length > 0)
        {
            ShowStep(0);
        }
    }

    private void Update()
    {
        if (currentIndex >= steps.Length) return;

        TutorialStep currentStep = steps[currentIndex];

        switch (currentStep.advanceMode)
        {
            case TutorialStep.AdvanceMode.Time:
                timer += Time.deltaTime;
                if (timer >= currentStep.timeDuration)
                {
                    NextStep();
                }
                break;

            case TutorialStep.AdvanceMode.Input:
                if (Input.GetKeyDown(currentStep.triggerKey))
                {
                    NextStep();
                }
                break;

            case TutorialStep.AdvanceMode.Manual:
                break;
        }
    }

    public void NextStep()
    {
        if (currentIndex < steps.Length - 1)
        {
            currentIndex++;
            ShowStep(currentIndex);
        }
        else
        {
            tooltipText.text = "";
            currentIndex++;
        }
    }

    public void SetStep(int index)
    {
        if (index < 0 || index >= steps.Length)
        {
            tooltipText.text = "";
            currentIndex = steps.Length;
            return;
        }

        currentIndex = index;
        ShowStep(currentIndex);
    }

    private void ShowStep(int index)
    {
        currentIndex = index;
        timer = 0f;
        
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypewriterEffect(steps[index].message));
        // play audio
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private IEnumerator TypewriterEffect(string message)
    {
        tooltipText.text = "";
        foreach (char letter in message.ToCharArray())
        {
            tooltipText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }
}