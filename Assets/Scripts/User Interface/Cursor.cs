using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public Sprite baseSprite;    
    public Sprite clickedSprite; 
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && baseSprite != null)
        {
            spriteRenderer.sprite = baseSprite;
        }
    }

    void Update()
    {
    
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(cursorPos.x, cursorPos.y);
        if (Input.GetMouseButtonDown(0)) 
        {
            if (spriteRenderer != null && clickedSprite != null)
            {
                spriteRenderer.sprite = clickedSprite;
            }
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            if (spriteRenderer != null && baseSprite != null)
            {
                spriteRenderer.sprite = baseSprite;
            }
        }
    }
}