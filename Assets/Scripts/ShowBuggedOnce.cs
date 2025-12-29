using UnityEngine;

public class ShowBuggedOnce : MonoBehaviour
{
    private GameManager GameManagerInstance;
    
    public Renderer[] myRenderer;
    public bool revealed = false;

    void Start()
    {
        GameManagerInstance = GameManager.Instance;
        myRenderer = GetComponentsInChildren<Renderer>();

        UpdateState();
    }

    void Update()
    {
        UpdateState();
    }

    void UpdateState()
    {
        bool isBugged = GameManagerInstance.getIsBugged(transform);

        if (isBugged && !revealed)
        {
            revealed = true;
        }
        if (revealed)
        {
            foreach (Renderer rend in myRenderer)
            {
                if (rend) rend.enabled = true;
            }
        }
        else 
        {
            foreach (Renderer rend in myRenderer)
            {
                if (rend) rend.enabled = false;
            }
        }
    }
}