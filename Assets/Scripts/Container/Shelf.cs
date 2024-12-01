using UnityEngine.EventSystems;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public class Shelf : Container
{
    [SerializeField] private SpriteRenderer highlightsSpriteRenderer;

    [Header("Animation")]
    [SerializeField] private Assets.Scripts.Animation.Animation m_animation;

    [Header("Sprites")]
    [SerializeField] private Sprite spriteHighlight0;
    [SerializeField] private Sprite spriteHighlight1;

    [Header("Sound effects")]
    [SerializeField] private AudioClip onTakeSound;
    [SerializeField] private AudioClip onDragSound;

    [HideInInspector] public bool isHoldingScroll = false;

    private bool _canHover = true;
    private bool _isDragging = false;

    protected Vector2 _basePosition;
    protected Vector2 _mousePositionFromObject;
    private Vector2 _holdingScrollBasePosition;

    private AudioSource m_audioSource;

    private GameObject _holdingScroll;

    private int _holdingScrollIndex;

    protected virtual void Start()
    {
        m_animation.UpdateAction = UpdateAnimation;
        m_animation.EndAction = EndAnimation;

        _basePosition = transform.position;

        m_audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        if (scrolls.Count == 0)
            return;

        if (_canHover)
        {
            m_animation.Update(Time.deltaTime);
        }

        if (_isDragging)
        {
            OnDragUpdate();
        }
    }

    public void ShuffleList()
    {
        scrolls = scrolls.OrderBy(x => Random.value).ToList();
    }

    private void TakeScroll()
    {
        _holdingScroll = scrolls[0].gameObject;
    }

    public virtual void OnBeginDrag()
    {
        if (scrolls.Count == 0)
            return;

        if (onTakeSound)
        {
            m_audioSource.clip = onTakeSound;
            m_audioSource.Play();
        }

        TakeScroll();

        FindFirstObjectByType<Desk>().EnableHovering();

        _holdingScrollBasePosition = _holdingScroll.transform.position;

        isHoldingScroll = true;
    }

    public virtual void OnEndDrag()
    {
        if (scrolls.Count == 0)
            return;

        if (onDragSound)
        {
            m_audioSource.clip = onDragSound;
            m_audioSource.Play();
        }

        Desk desk = FindFirstObjectByType<Desk>();

        if (desk.IsHovering())
        {
            Scroll scroll = scrolls[_holdingScrollIndex];

            scroll.OnDropOnDesk();

            desk.AddScroll(scroll);

            scrolls.RemoveAt(_holdingScrollIndex);
            _holdingScroll = null;
        }
        else
        {
            _holdingScroll.transform.position = _holdingScrollBasePosition;
        }

        FindFirstObjectByType<Desk>().DisableHovering();
    }

    public virtual void OnDragUpdate()
    {
        if (scrolls.Count == 0)
            return;

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _holdingScroll.transform.position = new Vector2(cursorPos.x, cursorPos.y);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (scrolls.Count == 0)
            return;

        if (!_canHover)
            return;

        highlightsSpriteRenderer.enabled = true;
        highlightsSpriteRenderer.sprite = spriteHighlight0;

        m_animation.StartAnimation();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (scrolls.Count == 0)
            return;

        if (!_canHover)
            return;

        highlightsSpriteRenderer.enabled = false;
    }

    private void UpdateAnimation(float t)
    {
        Sprite newSprite = spriteHighlight0;

        if (t >= .5f)
        {
            newSprite = spriteHighlight1;
        }

        highlightsSpriteRenderer.sprite = newSprite;
    }

    private void EndAnimation()
    {
        m_animation.StartAnimation();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (scrolls.Count == 0)
            return;

        _canHover = false;
        _isDragging = true;

        highlightsSpriteRenderer.enabled = false;

        OnBeginDrag();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (scrolls.Count == 0)
            return;

        _canHover = true;
        _isDragging = false;

        transform.position = _basePosition;

        OnEndDrag();
    }

    public override bool AddScroll(Scroll scroll)
    {
        if (!base.AddScroll(scroll)) return false;
        scroll.HideCompleteSprite();
        return true;
    }
}
