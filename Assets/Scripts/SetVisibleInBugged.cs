using UnityEngine;

public class SetVisibleInBugged : MonoBehaviour
{
  GameManager gameManagerInstance;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    gameManagerInstance = GameManager.Instance;
  }

  // Update is called once per frame
  void Update()
  {
    if (gameManagerInstance.getDebugMode())
    {
      foreach (Transform child in transform)
      {
        Debug.Log(gameObject.activeInHierarchy);

        Debug.Log(child);
        child.gameObject.SetActive(true);
      }
    }

    else
      foreach (Transform child in transform)
      {
        child.gameObject.SetActive(false);
      }
  }
}
