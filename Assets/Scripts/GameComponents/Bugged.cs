using UnityEngine;

public class Bugged : MonoBehaviour
{
    private GameManager GameManagerInstance;
    [SerializeField] bool reverse = false;
    
    private Renderer myRenderer;
    private Collider myCollider;

    void Start()
    {
        GameManagerInstance = GameManager.Instance;
        myRenderer = GetComponent<Renderer>();
        myCollider = GetComponent<Collider>();
        
        UpdateState();
    }

    void Update()
    {
        UpdateState();
    }

    void UpdateState()
    {
        bool isBugged = GameManagerInstance.getIsBugged(transform);
        
        bool shouldBeVisible = reverse ? !isBugged : isBugged;

        if(myRenderer) myRenderer.enabled = shouldBeVisible;
        if(myCollider) myCollider.enabled = shouldBeVisible;
    }
}