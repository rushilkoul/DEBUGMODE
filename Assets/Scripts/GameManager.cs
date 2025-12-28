using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; set; }
  [SerializeField] private bool debugMode;
  [SerializeField] private float range = 50;
  [SerializeField] private Transform playerPos;

  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public bool getIsBugged(Transform buggedTransform)
  {
    float distance = Vector3.Distance(playerPos.position, buggedTransform.position);
    return debugMode && distance < range;
  }

  public bool getDebugMode()
  {
    return debugMode;
  }


  public void setDebugMode(bool val)
  {
    debugMode = val;
  }
}
