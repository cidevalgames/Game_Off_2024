using UnityEngine.EventSystems;
using UnityEngine;

public class Desk : Container
{
    [SerializeField] private SpriteRenderer highlightsSpriteRenderer;
    [SerializeField] private GameObject scrollOnDesk;
    [SerializeField] private SpriteRenderer scrollOnDeskSpriteRenderer;

    [Header("Animation")]
    [SerializeField] private Assets.Scripts.Animation.Animation m_animation;

    [Header("Sprites")]
    [SerializeField] private Sprite spriteHighlight0;
    [SerializeField] private Sprite spriteHighlight1;

    [Header("Sound effects")]
    [SerializeField] private AudioClip onTakeSound;
    [SerializeField] private AudioClip onDragSound;

    [HideInInspector] public bool isHoldingScroll = false;

    private bool _canHover = false;

    private GameObject _holdingScroll;

    protected Vector2 _basePosition;
    protected Vector2 _mousePositionFromObject;
    private Vector2 _holdingScrollBasePosition;

    private AudioSource m_audioSource;

    private bool _isHovering;

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

    private void TakeScroll()
    {
        _holdingScroll = scrolls[0].gameObject;
    }

    public virtual void OnBeginDrag()
    {
        if (onTakeSound)
        {
            m_audioSource.clip = onTakeSound;
            m_audioSource.Play();
        }

        TakeScroll();

        FindFirstObjectByType<VillageRack>().EnableHovering();
        FindFirstObjectByType<VilleRack>().EnableHovering();

        _holdingScrollBasePosition = _holdingScroll.transform.position;

        isHoldingScroll = true;
    }

    public virtual void OnEndDrag()
    {
        if (onDragSound)
        {
            m_audioSource.clip = onDragSound;
            m_audioSource.Play();
        }

        VillageRack villageRack = FindFirstObjectByType<VillageRack>();
        VilleRack villeRack = FindFirstObjectByType<VilleRack>();

        if (villageRack.IsHovering())
        {
            Scroll scroll = scrolls[0];

            scroll.OnDropOnDesk();

            villageRack.AddScroll(scroll);

            scrolls.RemoveAt(0);
            _holdingScroll = null;
        }
        else if (villeRack.IsHovering())
        {
            Scroll scroll = scrolls[0];

            scroll.OnDropOnDesk();

            villeRack.AddScroll(scroll);

            scrolls.RemoveAt(0);
            _holdingScroll = null;
        }
        else
        {
            _holdingScroll.transform.position = _holdingScrollBasePosition;
        }

        FindFirstObjectByType<VillageRack>().DisableHovering();
        FindFirstObjectByType<VilleRack>().DisableHovering();
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
        if (!_canHover)
            return;

        _isHovering = true;

        highlightsSpriteRenderer.enabled = true;
        highlightsSpriteRenderer.sprite = spriteHighlight0;

        m_animation.StartAnimation();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!_canHover)
            return;

        _isHovering = false;

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

        highlightsSpriteRenderer.enabled = false;

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

    public override bool AddScroll(Scroll scroll)
    {
        if (!base.AddScroll(scroll)) return false;

        scrollOnDesk.SetActive(true);
        scroll.transform.SetParent(scrollOnDeskSpriteRenderer.transform.parent, false);
        scroll.transform.localScale = Vector3.one * 1.150722f;
        scroll.transform.localPosition = new Vector3(0, -10.58f, 2.734f);

        return true;
    }
}
