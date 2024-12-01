using UnityEngine.EventSystems;
using UnityEngine;

public class Desk : Container
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

    private bool _canHover = true;

    protected Vector2 _basePosition;
    protected Vector2 _mousePositionFromObject;

    private AudioSource m_audioSource;

    protected virtual void Start()
    {
        m_animation.UpdateAction = UpdateAnimation;
        m_animation.EndAction = EndAnimation;

        _basePosition = transform.position;

        m_audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        if (_canHover)
        {
            m_animation.Update(Time.deltaTime);
        }
    }

    public virtual void OnBeginDrag()
    {
        if (onTakeSound)
        {
            m_audioSource.clip = onTakeSound;
            m_audioSource.Play();
        }
    }

    public virtual void OnEndDrag()
    {
        if (onDragSound)
        {
            m_audioSource.clip = onDragSound;
            m_audioSource.Play();
        }
    }

    public virtual void OnDragUpdate()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(cursorPos.x, cursorPos.y) - _mousePositionFromObject;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!_canHover)
            return;

        highlightsSpriteRenderer.enabled = true;
        highlightsSpriteRenderer.sprite = spriteHighlight0;

        m_animation.StartAnimation();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
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
        _canHover = false;

        highlightsSpriteRenderer.enabled = false;

        _mousePositionFromObject = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        OnBeginDrag();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        _canHover = true;

        transform.position = _basePosition;

        OnEndDrag();
    }

    protected override bool IsScrollValid(Scroll scroll)
    {
        return scroll != null && !scrolls.Contains(scroll) && scrolls.Count == 0;
    }

    protected override bool AddScroll(Scroll scroll)
    {
        if (!base.AddScroll(scroll)) return false;
        scroll.ShowCompleteSprite();
        return true;
    }
}
