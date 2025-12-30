using System;
using UnityEngine;

public class SetVisibleInBugged : MonoBehaviour
{
  GameManager gameManagerInstance;
  [SerializeField] private bool buggedOnce;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    gameManagerInstance = GameManager.Instance;
    GameManager.Instance.OnDebugModeChanged.AddListener(SetChildren);
    if (buggedOnce)
    {
      foreach (Transform child in transform)
      {
        child.gameObject.SetActive(false);
      }
    }

  }

  private void SetChildren(bool debugMode)
  {
    if (buggedOnce)
    {
      if (debugMode)
        foreach (Transform child in transform)
          if (gameManagerInstance.getIsBugged(child))
            child.gameObject.SetActive(true);
      return;
    }

    if (debugMode)
      foreach (Transform child in transform)
        child.gameObject.SetActive(true);

    else
      foreach (Transform child in transform)
        child.gameObject.SetActive(false);
  }
}
