using UnityEngine;

public class Bugged : MonoBehaviour
{
  [SerializeField] private Transform buggedIndicatorVisual;

  private GameManager GameManagerInstance;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    GameManagerInstance = GameManager.Instance;
    buggedIndicatorVisual.gameObject.SetActive(false);
  }


  // Update is called once per frame
  void Update()
  {
    if (GameManagerInstance.getIsBugged(transform))
    {
      buggedIndicatorVisual.gameObject.SetActive(true);
    }
    else
    {
      buggedIndicatorVisual.gameObject.SetActive(false);
    }
  }
}
