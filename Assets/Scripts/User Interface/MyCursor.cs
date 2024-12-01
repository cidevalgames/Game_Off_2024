using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyCursor : MonoBehaviour
{
    [Header("Cursor textures")]
    [SerializeField] private Sprite normalCursorSprite;
    [SerializeField] private Sprite clickCursorSprite;

    private bool _cursorEnabled = false;

    private SpriteRenderer m_spriteRenderer;

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        if (m_spriteRenderer != null && normalCursorSprite != null)
        {
            m_spriteRenderer.sprite = normalCursorSprite;
        }

        //EnableCursor();
    }

    private void Update()
    {
        if (!_cursorEnabled)
            return;

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(cursorPos.x, cursorPos.y);

        if (Input.GetMouseButtonDown(0))
        {
            if (m_spriteRenderer != null && clickCursorSprite != null)
            {
                m_spriteRenderer.sprite = clickCursorSprite;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (m_spriteRenderer != null && normalCursorSprite != null)
            {
                m_spriteRenderer.sprite = normalCursorSprite;
            }
        }
    }

    public void EnableCursor()
    {
        _cursorEnabled = true;

        Cursor.visible = false;
    }

    public void DisableCursor()
    {
        _cursorEnabled = false;

        Cursor.visible = true;
    }

}