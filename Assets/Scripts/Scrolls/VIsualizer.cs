using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    [Header("Scroll Settings")]
    [Tooltip("Percentage of the object's height to scroll per scroll step.")]
    [Range(0, 1)]
    public float scrollPercentage = 0.5f; // 50% of the height by default

    [Tooltip("Speed at which the object scrolls.")]
    public float scrollSpeed = 5f;

    [Header("Detection Settings")]
    [Tooltip("Enable or disable scroll detection for this object.")]
    public bool isScrollable = true;

    private bool isMouseOver = false;
    private Vector3 targetPosition;

    private Collider2D collider2D;

    void Start()
    {
        // Initialize the target position to the object's current position
        targetPosition = transform.position;

        // Cache the Collider2D component
        collider2D = GetComponent<Collider2D>();
        if (collider2D == null)
        {
            Debug.LogError("A 2D Collider is required for mouse detection.");
        }
    }

    void Update()
    {
        // Detect if the mouse is over the 2D collider
        if (collider2D != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMouseOver = collider2D.OverlapPoint(mousePosition);
        }

        // Handle scrolling
        if (isScrollable && isMouseOver)
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0)
            {
                Debug.Log($"Scroll Input Detected: {scrollInput}");

                // Calculate scroll offset based on percentage and object's height
                float offset = scrollInput * scrollPercentage * transform.localScale.y;

                // Update the target position
                targetPosition += new Vector3(0, offset, 0);
            }
        }

        // Smoothly move the object towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * scrollSpeed);
    }
}

