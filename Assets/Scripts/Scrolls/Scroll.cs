using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        closedSprite = m_spriteRenderer.sprite;
    }

    public void OnDropOnDesk()
    {
        onDropOnDesk.Invoke();
    }

    public void OnDropInRack()
    {
        onDropInRack.Invoke();
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
}