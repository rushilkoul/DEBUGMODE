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
    public GameObject explosionPrefab; // Assign a particle prefab for when it dies

    private BoxCollider barrierCollider;
    private List<Vector3> waypoints = new List<Vector3>();
    private int currentTargetIndex = 0;
    private bool isFixed = false;

    void Start()
    {
        barrierCollider = GetComponent<BoxCollider>();
        CalculateCorners();

        // Snap bug to start position immediately
        if (waypoints.Count > 0 && bugObject != null)
        {
            bugObject.position = waypoints[0];
        }
    }

    void Update()
    {
        if (isFixed || bugObject == null || waypoints.Count == 0) return;

        // 1. Move Bug towards current target corner
        Vector3 target = waypoints[currentTargetIndex];
        Vector3 newPos = Vector3.MoveTowards(bugObject.position, target, speed * Time.deltaTime);
        bugObject.position = newPos;

        // 2. Check if reached corner
        if (Vector3.Distance(bugObject.position, target) < turnThreshold)
        {
            // Hit a corner!
            OnCornerHit();
            
            // Go to next corner (Loop around: 0 -> 1 -> 2 -> 3 -> 0)
            currentTargetIndex = (currentTargetIndex + 1) % waypoints.Count;
        }
    }

    void CalculateCorners()
    {
        // Get local bounds relative to the center
        Vector3 center = barrierCollider.center;
        Vector3 size = barrierCollider.size;

        // Calculate the 4 corners of the "face" of the box (assuming Z is the thin axis)
        // We work in Local Space first, then transform to World Space
        float x = size.x / 2;
        float y = size.y / 2;
        
        // We offset slightly inward (-0.1f) so the bug is barely inside the collider
        // Adjust this depending on your bug size
        float padding = 0.0f; 

        waypoints.Add(transform.TransformPoint(center + new Vector3(-x + padding, y - padding, 0))); // Top Left
        waypoints.Add(transform.TransformPoint(center + new Vector3(x - padding, y - padding, 0)));  // Top Right
        waypoints.Add(transform.TransformPoint(center + new Vector3(x - padding, -y + padding, 0))); // Bottom Right
        waypoints.Add(transform.TransformPoint(center + new Vector3(-x + padding, -y + padding, 0)));// Bottom Left
    }

    void OnCornerHit()
    {
        // Visual flair when turning a corner
        if (cornerSparks != null)
        {
            cornerSparks.Play();
        }
        
        // Optional: Screenshake or sound here
    }

    // Call this to "Fix" the bug (Player Interaction)
    public void FixBug()
    {
        if (isFixed) return;
        isFixed = true;

        // 1. Disable the physical wall
        barrierCollider.enabled = false;

        // 2. Visual Explosion
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, bugObject.position, Quaternion.identity);
        }

        // 3. Destroy the bug loop
        Destroy(bugObject.gameObject); // Kill the bug
        
        // Optional: Destroy the parent too after a delay
        Destroy(gameObject, 1.0f);
    }
}