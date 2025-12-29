using UnityEngine;
using UnityEngine.Events;

public class ColliderTriggerEvent : MonoBehaviour
{
    public UnityEvent eventToRun;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            eventToRun.Invoke();
            gameObject.SetActive(false);
        }
    }
}