using UnityEngine;
using UnityEngine.Events;

public class DrainCompleteLogic : MonoBehaviour
{
    public UnityEvent eventToRun;
    public void OnComplete()
    {
        eventToRun.Invoke();
    }
}