using UnityEngine;

public class Visualizer : MonoBehaviour
{
    [Header("Scroll Settings")]
    [Tooltip("Percentage of the object's height to scroll per scroll step.")]
    [Range(0, 15)]
    public float scrollPercentage = 0.5f;

    [Tooltip("Speed at which the object scrolls.")]
    public float scrollSpeed = 5f;

    [Header("Scale Settings")]
    [Tooltip("Minimum scale factor (as a percentage of the original scale).")]
    [Range(0.1f, 1f)]
    public float minScaleFactor = 0.8f; // 80% of the original scale at max scroll

    [Header("Detection Settings")]
    [Tooltip("Enable or disable scroll detection for this object.")]
    public bool isScrollable = true;

    [Tooltip("Reference sprite used to calculate max Y scroll.")]
    public SpriteRenderer referenceSprite;

    private bool isMouseOver = false;
    private Vector3 targetPosition;
    private Collider2D collider2D;
    private float baseY;
    private float maxY;
    private Vector3 originalScale;

    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        if (collider2D == null)
        {
            Debug.LogError("A 2D Collider is required for mouse detection.");
            return;
        }

        if (referenceSprite == null)
        {
            Debug.LogError("A reference sprite is required to calculate the max Y scroll.");
            return;
        }

        baseY = transform.position.y;
        BoxCollider2D boxCollider = collider2D as BoxCollider2D;
        if (boxCollider != null)
        {
            float referenceHeight = referenceSprite.bounds.size.y;
            float scrollObjectHeight = boxCollider.size.y;
            float divisionCount = scrollObjectHeight / referenceHeight;

            maxY = baseY + divisionCount;
        }
        else
        {
            Debug.LogError("Collider2D is not a BoxCollider2D. This script requires a BoxCollider2D.");
        }

        targetPosition = transform.position;
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (collider2D != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMouseOver = collider2D.OverlapPoint(mousePosition);
        }

        if (isScrollable && isMouseOver)
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0)
            {
                scrollInput *= -1;
                BoxCollider2D boxCollider = collider2D as BoxCollider2D;
                if (boxCollider != null)
                {
                    float offset = scrollInput * scrollPercentage * boxCollider.size.y;
                    targetPosition += new Vector3(0, offset, 0);
                    targetPosition.y = Mathf.Clamp(targetPosition.y, baseY, maxY);
                }
            }
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * scrollSpeed);

        UpdateScale();
    }

    private void UpdateScale()
    {
        float scrollProgress = Mathf.InverseLerp(baseY, maxY, transform.position.y);
        float scaleFactor = Mathf.Lerp(1f, minScaleFactor, scrollProgress);
        transform.localScale = originalScale * scaleFactor;
    }
}
