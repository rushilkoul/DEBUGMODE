using UnityEngine;

// The attribute below adds a menu option to create the asset in the Unity Editor
[CreateAssetMenu(fileName = "BugArr", menuName = "Game Data/BugArr")]
public class BugArrSO : ScriptableObject
{
  public Transform[] bugTransformArr;
}
