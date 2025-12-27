using UnityEngine;

public class isBugged : MonoBehaviour
{
  [SerializeField] private bool debugMode = false;
  [SerializeField] private Transform buggedIndicatorVisual;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (debugMode)
    {
      buggedIndicatorVisual.gameObject.SetActive(true);
    }
    else
    {
      buggedIndicatorVisual.gameObject.SetActive(false);
    }

  }
}
