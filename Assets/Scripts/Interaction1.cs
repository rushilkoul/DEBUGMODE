using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class WallUIInteraction : MonoBehaviour
{
    public float interactionRange = 5f;
    public LayerMask uiLayerMask;
    
    private bool isLookingAtUI = false;
    private Canvas currentCanvas;
    private GraphicRaycaster graphicRaycaster;
    
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, interactionRange, uiLayerMask))
        {
            // Get canvas
            currentCanvas = hit.collider.GetComponent<Canvas>();
            
            if (currentCanvas != null && !isLookingAtUI)
            {
                isLookingAtUI = true;
                graphicRaycaster = currentCanvas.GetComponent<GraphicRaycaster>();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            
            // Handle UI interaction
            if (isLookingAtUI && graphicRaycaster != null)
            {
                CheckUIInteraction();
            }
        }
        else
        {
            if (isLookingAtUI)
            {
                isLookingAtUI = false;
                currentCanvas = null;
                graphicRaycaster = null;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
    
    void CheckUIInteraction()
    {
        // Create pointer event data
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;
        
        // Raycast using graphic raycaster
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerData, results);
        
        // Debug to see if we're hitting UI elements
        if (results.Count > 0)
        {
            Debug.Log("Hovering over: " + results[0].gameObject.name);
            
            // Handle hover
            if (results[0].gameObject != null)
            {
                ExecuteEvents.Execute(results[0].gameObject, pointerData, 
                    ExecuteEvents.pointerEnterHandler);
            }
            
            // Handle clicks
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Clicking: " + results[0].gameObject.name);
                ExecuteEvents.Execute(results[0].gameObject, pointerData, 
                    ExecuteEvents.pointerDownHandler);
                ExecuteEvents.Execute(results[0].gameObject, pointerData, 
                    ExecuteEvents.pointerClickHandler);
            }
            
            // Handle mouse up
            if (Input.GetMouseButtonUp(0))
            {
                ExecuteEvents.Execute(results[0].gameObject, pointerData, 
                    ExecuteEvents.pointerUpHandler);
            }
            
            // Handle dragging (important for sliders!)
            if (Input.GetMouseButton(0))
            {
                ExecuteEvents.Execute(results[0].gameObject, pointerData, 
                    ExecuteEvents.dragHandler);
                ExecuteEvents.Execute(results[0].gameObject, pointerData, 
                    ExecuteEvents.beginDragHandler);
            }
        }
    }
}