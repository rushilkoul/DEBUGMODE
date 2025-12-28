// using System.Diagnostics;
using System;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float radius = 2f;
    public Color gizmoColor = Color.red;

    public bool showOnlyWhenSelected = false;
    public float fillOpacity = 0.2f;

    public Transform player;
    public GameObject panelElement;
    public InteractionBehaviour interaction;
    public bool interactionActive = false;

    private bool _isPlayerInRange = false;

    private void OnDrawGizmos()
    {
        if (!showOnlyWhenSelected)
        {
            DrawRangeGizmo();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (showOnlyWhenSelected)
        {
            DrawRangeGizmo();
        }
    }

    private void DrawRangeGizmo()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, radius);

        Color fillColor = gizmoColor;
        fillColor.a = fillOpacity;
        Gizmos.color = fillColor;

        Gizmos.DrawSphere(transform.position, radius);
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        bool currentlyInRange = dist < radius;

        if(currentlyInRange != _isPlayerInRange)
        {
            _isPlayerInRange = currentlyInRange;

            if(_isPlayerInRange)
            {
                panelElement.SetActive(true);
            }
            else
            {
                panelElement.SetActive(false);   
            }
        }

        if (_isPlayerInRange)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.E) && interactionActive == true)
        {
            interactionActive = false;
            interaction.OnDoubleInput();
            // panelElement.SetActive(true);  
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            interactionActive = true;
            interaction.OnInput();
            // panelElement.SetActive(false);  
        }
    }

}
