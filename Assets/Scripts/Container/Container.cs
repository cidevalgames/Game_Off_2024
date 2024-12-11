using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Desk.Objects;
using Assets.Scripts.Animation;

[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public abstract class Container : DevObject, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IDropHandler
{
    [Header("Parchemins présents dans le conteneur au lancement du jeu")]
    [SerializeField]
    protected List<Scroll> scrolls;

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

    protected bool _canHover = true;

    private bool _isHovering;

    protected Vector2 _basePosition;
    private Vector2 _scrollBasePosition;

    private GameObject _holdingScroll;

    private int _holdingScrollIndex;

    #region Primary Functions

    protected virtual void Start()
    {
        m_animation.UpdateAction = UpdateAnimation;
        m_animation.EndAction = EndAnimation;

        _basePosition = transform.position;
    }

    protected virtual void Update()
    {
        if (_canHover)
        {
            m_animation.Update(Time.deltaTime);
        }
    }

    #endregion

    #region Dragging

    public virtual void OnEndDrag()
    {
        if (scrolls.Count == 0)
            return;

        scrolls[0].OnEndDrag(this);
    }
    #endregion

    #region Event Systems
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        print("Hovering");

        if (!_canHover)
            return;

        _isHovering = true;

        highlightsSpriteRenderer.enabled = true;
        highlightsSpriteRenderer.sprite = spriteHighlight0;

        m_animation.StartAnimation();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (!_canHover)
            return;

        _isHovering = false;

        highlightsSpriteRenderer.enabled = false;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        _canHover = false;

        highlightsSpriteRenderer.enabled = false;

        scrolls[0].OnBeginDrag(this);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        _canHover = true;

        transform.position = _basePosition;

        OnEndDrag();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        scrolls[0].OnDrag();
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        
    }

    #endregion

    #region Animation
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
    #endregion

    public void EnableHovering()
    {
        _canHover = true;
    }

    public void DisableHovering()
    {
        _canHover = false;
    }

    public bool IsHovering()
    {
        return _isHovering;
    }

    #region Scrolls

    protected void TransferScrollTo(Scroll scroll, Container transferTarget)
    {
        if (!transferTarget.AddScroll(scroll)) return;

        scrolls.Remove(scroll);

    }

    public virtual bool AddScroll(Scroll scroll)
    {
        if (!IsScrollValid(scroll)) return false;
        scrolls.Add(scroll);
        return true;
    }

    public virtual void RemoveScroll(Scroll scroll)
    {
        scrolls.Remove(scroll);
    }

    protected virtual bool IsScrollValid(Scroll scroll)
    {
        return scroll != null && !scrolls.Contains(scroll);
    }

    #endregion
}
