using UnityEngine;
using System.Collections.Generic;

public class InfiniteLoopbug : MonoBehaviour
{
    [Header("Configuration")]
    public Transform bugObject;
    public float speed = 10f;
    [Tooltip("How close to the corner before snapping to the next one")]
    public float turnThreshold = 0.1f;

    [Header("Visual Effects")]
    public ParticleSystem cornerSparks;
    public TrailRenderer bugTrail;
    public GameObject explosionPrefab;

    private BoxCollider barrierCollider;
    private List<Vector3> waypoints = new List<Vector3>();
    private int currentTargetIndex = 0;
    private bool isFixed = false;

    void Start()
    {
        barrierCollider = GetComponent<BoxCollider>();
        CalculateCorners();
        if (waypoints.Count > 0 && bugObject != null)
        {
            bugObject.position = waypoints[0];
        }
    }

    void Update()
    {
        if (isFixed || bugObject == null || waypoints.Count == 0) return;

        Vector3 target = waypoints[currentTargetIndex];
        Vector3 newPos = Vector3.MoveTowards(bugObject.position, target, speed * Time.deltaTime);
        bugObject.position = newPos;
        if (Vector3.Distance(bugObject.position, target) < turnThreshold)
        {
            OnCornerHit();
            currentTargetIndex = (currentTargetIndex + 1) % waypoints.Count;
        }
    }

    void CalculateCorners()
    {
        Vector3 center = barrierCollider.center;
        Vector3 size = barrierCollider.size;
        float x = size.x / 2;
        float y = size.y / 2;

        float padding = 0.0f;

        waypoints.Add(transform.TransformPoint(center + new Vector3(-x + padding, y - padding, 0)));
        waypoints.Add(transform.TransformPoint(center + new Vector3(x - padding, y - padding, 0)));  
        waypoints.Add(transform.TransformPoint(center + new Vector3(x - padding, -y + padding, 0))); 
        waypoints.Add(transform.TransformPoint(center + new Vector3(-x + padding, -y + padding, 0)));
    }

    void OnCornerHit()
    {
        if (cornerSparks != null)
        {
            cornerSparks.Play();
        }

    }
    public void FixBug()
    {
        if (isFixed) return;
        isFixed = true;
        barrierCollider.enabled = false;
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, bugObject.position, Quaternion.identity);
        }
        Destroy(bugObject.gameObject);

        Destroy(gameObject, 1.0f);
    }
}