using UnityEngine;

public class DrawLines : MonoBehaviour
{
    private Texture2D m_Texture;
    private Color[] m_Colors;
    private RaycastHit2D hit;
    private SpriteRenderer spriteRend;
    private BoxCollider2D boxCollider;
    private Color zeroAlpha = Color.clear; // Transparent color (used for erasing)
    private Color drawColor = Color.black; // Color to draw on the sprite (you can change this as needed)
    public int drawSize = 10; // Draw size (this will determine the drawing "brush" size)
    public bool Drawing = false;

    // Store original values to preserve sprite and collider properties
    private Vector2 originalColliderSize;
    private Vector2 originalSpriteSize;
    private float originalPixelsPerUnit;
    private Vector3 originalScale;

    void Start()
    {
        // Initialize the sprite renderer, texture, and box collider
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();

        // Store the original material, collider size, pixels per unit, and scale
        originalScale = spriteRend.transform.localScale; // Store scale
        originalColliderSize = boxCollider.size;
        originalPixelsPerUnit = spriteRend.sprite.pixelsPerUnit;

        // Get original sprite size (in pixels)
        originalSpriteSize = spriteRend.sprite.rect.size;

        // Create a new texture with the same dimensions as the original sprite texture
        Texture2D tex = spriteRend.sprite.texture;
        m_Texture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        m_Texture.filterMode = FilterMode.Point;
        m_Texture.wrapMode = TextureWrapMode.Clamp;

        // Initialize the texture with transparent color (erasing all pixels initially)
        m_Colors = new Color[tex.width * tex.height];
        for (int i = 0; i < m_Colors.Length; i++)
            m_Colors[i] = zeroAlpha; // Set all pixels to transparent

        // Apply transparency to the texture and then set it to the sprite
        m_Texture.SetPixels(m_Colors);
        m_Texture.Apply();

        // Create a sprite with the transparent texture and set it to the SpriteRenderer
        Rect originalRect = spriteRend.sprite.rect; // The original sprite's rect
        spriteRend.sprite = Sprite.Create(m_Texture, originalRect, new Vector2(0.5f, 0.5f), originalPixelsPerUnit);

        // Restore the original collider size to ensure it matches the sprite's size
        boxCollider.size = originalColliderSize;

        // Restore the original scale of the SpriteRenderer
        spriteRend.transform.localScale = originalScale;
    }

    void Update()
    {
        // Detect when the user clicks the mouse (left click)
        if (Input.GetMouseButton(0))
        {
            // Perform the raycast to detect collision with the sprite
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            // Check if the raycast hit a collider with the "Untagged" tag (or your custom tag)
            if (hit.collider != null && hit.collider.CompareTag("Untagged")) // Change to "Drawable" if you create a custom tag
            {
                UpdateTexture(hit.point);
                Drawing = true;
            }
        }
        else
        {
            Drawing = false;
        }
    }

    public void UpdateTexture(Vector2 mouseWorldPos)
    {
        int w = m_Texture.width;
        int h = m_Texture.height;

        // Convert the mouse world position into local position relative to the collider
        Vector2 mousePos = mouseWorldPos - (Vector2)hit.collider.bounds.min;

        // Map the mouse position to the texture coordinates, accounting for sprite scaling
        mousePos.x *= w / hit.collider.bounds.size.x;
        mousePos.y *= h / hit.collider.bounds.size.y;

        Vector2Int p = new Vector2Int((int)mousePos.x, (int)mousePos.y);

        // Draw a circle or square around the mouse position
        DrawShape(p, w, h);

        // Apply the modified texture to the sprite
        m_Texture.SetPixels(m_Colors);
        m_Texture.Apply();

        // Update the sprite's texture while keeping original pixelsPerUnit
        spriteRend.sprite = Sprite.Create(m_Texture, spriteRend.sprite.rect, new Vector2(0.5f, 0.5f), originalPixelsPerUnit);

        // Restore the original scale of the SpriteRenderer
        spriteRend.transform.localScale = originalScale;
    }

    // Draw a circle (or square) at the given position
    private void DrawShape(Vector2Int p, int w, int h)
    {
        // Loop over a square area around the mouse position
        int size = drawSize;
        for (int x = -size; x < size; x++)
        {
            for (int y = -size; y < size; y++)
            {
                Vector2Int pixel = new Vector2Int(p.x + x, p.y + y);
                // Ensure the pixel is within bounds
                if (pixel.x >= 0 && pixel.x < w && pixel.y >= 0 && pixel.y < h)
                {
                    // Draw a square or circle
                    if (drawSize > 0)
                    {
                        if (drawSize > 1)
                        {
                            // For circles: check if within circle radius
                            if ((x * x + y * y) <= (size * size))
                            {
                                m_Colors[pixel.x + pixel.y * w] = drawColor; // Set the pixel color
                            }
                        }
                        else
                        {
                            // For squares: just draw a square shape around the center
                            m_Colors[pixel.x + pixel.y * w] = drawColor;
                        }
                    }
                }
            }
        }
    }
}
