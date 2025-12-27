using UnityEngine;

public class isBugged : MonoBehaviour
{
  [SerializeField] private Transform buggedIndicatorVisual;
  private GameManager GameManagerInstance;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    GameManagerInstance = GameManager.Instance;
  }

  // Update is called once per frame
  void Update()
  {
    if (GameManagerInstance.getIsDebugMode(transform))
    {
      buggedIndicatorVisual.gameObject.SetActive(true);
    }
    else
    {
      buggedIndicatorVisual.gameObject.SetActive(false);
    }
  }
}
