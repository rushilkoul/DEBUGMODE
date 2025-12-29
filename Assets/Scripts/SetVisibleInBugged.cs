using System;
using UnityEngine;

public class SetVisibleInBugged : MonoBehaviour
{
  GameManager gameManagerInstance;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    gameManagerInstance = GameManager.Instance; GameManager.Instance.OnDebugModeChanged.AddListener(SetChildren);

  }

  private void SetChildren(bool debugMode)
  {
    if (debugMode)
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

  // Update is called once per frame
  void Update()
  {
  }
}
