using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Scroll : DevObject
{
    [SerializeField]
    [Header("Destination pour fin conspiration")]
    private EScrollDestination cDestination;

    [SerializeField]
    [Header("Destination pour fin héroïque")]
    private EScrollDestination hDestination;

    [Header("Sprite du parchemin déroulé")]
    public Sprite openSprite;

    [Header("")]
    [SerializeField] private UnityEvent onDropOnDesk;
    [SerializeField] private UnityEvent onDropInRack;

    private Sprite closedSprite;

    private SpriteRenderer m_spriteRenderer;

    private bool _isDragging;

    private Vector2 _basePos;
    public Vector3 baseScale;
    public Vector3 secondScale;

    private Shelf m_shelf;
    private Desk m_desk;
    private VillageRack m_villageRack;
    private VilleRack m_villeRack;

    private Rack[] racks = new Rack[2];

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        closedSprite = m_spriteRenderer.sprite;

        m_shelf = FindFirstObjectByType<Shelf>();
        m_desk = FindFirstObjectByType<Desk>();
        m_villageRack = FindFirstObjectByType<VillageRack>();
        m_villeRack = FindFirstObjectByType<VilleRack>();

        racks = FindObjectsByType<Rack>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }

    #region Dragging

    public void OnBeginDrag(Container container)
    {
        if (container == m_shelf)
        {
            m_desk.EnableHovering();
        }
        else if (container == m_desk)
        {
            m_villageRack.EnableHovering();
            m_villeRack.EnableHovering();

            transform.localScale = baseScale;

            HideCompleteSprite();
        }

        _isDragging = true;

        _basePos = transform.position;
    }

    public void OnEndDrag(Container container)
    {
        _isDragging = false;

        // Ajoute ce scroll selon d'où il vient et où il est déposé
        if (container == m_shelf && m_desk.IsHovering())
        {
            //m_desk.AddScroll(this);
            //m_desk.DisableHovering();

            //container.RemoveScroll(this);

            //ShowCompleteSprite();

            //onDropOnDesk.Invoke();
        }
        else if (container == m_desk)
        {
            if (m_villageRack.IsHovering())
            {
                //m_villageRack.AddScroll(this);
                //m_villageRack.DisableHovering();

                //m_villageRack.RemoveScroll(this);

                //onDropInRack.Invoke();
            }
            else if (m_villeRack.IsHovering())
            {
                //m_villeRack.AddScroll(this);
                //m_villeRack.DisableHovering();

                //m_villageRack.RemoveScroll(this);

                //onDropInRack.Invoke();
            }
            else
            {
                ShowCompleteSprite();

                transform.localScale = secondScale;
                transform.position = _basePos;
            }
        }
        else
        {
            transform.position = _basePos;
        }
    }

    #endregion

    public void OnDrag()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(cursorPos.x, cursorPos.y);
    }

    public void ShowCompleteSprite()
    {
        m_spriteRenderer.sprite = openSprite;
    }

    public void HideCompleteSprite()
    {
        m_spriteRenderer.sprite = closedSprite;
    }

    private void Start()
    {
        HideCompleteSprite();
    }

    private void Update()
    {
        if (!_isDragging)
            return;

        ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.dragHandler);
    }
}