using System;
using Unity.VisualScripting;
using UnityEngine;

public class Scroll : DevObject
{
    [SerializeField]
    [Header("Destination pour fin conspiration")]
    private EScrollDestination cDestination;

    [SerializeField]
    [Header("Destination pour fin héroïque")]
    private EScrollDestination hDestination;

    [SerializeField]
    [Header("Sprite du parchemin déroulé")]
    private Sprite sprite;

    private Sprite _defaultSprite;

    const string DefaultSpritePath = "Sprites/Scenes/Bureau/Objects/parchemin clair";

    public void ShowCompleteSprite()
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void HideCompleteSprite()
    {
        GetComponent<SpriteRenderer>().sprite = _defaultSprite;
    }

    private void Awake()
    {
        _defaultSprite = Resources.Load<Sprite>(DefaultSpritePath);
        this.AddComponent<SpriteRenderer>();
    }

    private void Start()
    {
        HideCompleteSprite();
    }
}