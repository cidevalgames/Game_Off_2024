using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogTextPanel : DevObject, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI textToSet;

    [HideInInspector]
    public List<string> textQueue = new List<string>();

    private int _nextTextIndex = 0;
    
    private void Show()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            spriteRenderer.enabled = true;
        }
    }
    
    private void Hide()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            spriteRenderer.enabled = false;
        }
    }

    public void Dialog(int index)
    {
        if (index > textQueue.Count - 1) // Il n'y a plus de dialogues à afficher
        {
            Hide();
            ResetQueue();
            GameState().monk.Disappear();
            return;
        }
        if (index == 0)
        {
            _nextTextIndex = 1;
            SetText(textQueue[0]);
            Show();
            return;
        }
        SetText(textQueue[index]);
        _nextTextIndex++;
    }

    private void SetText(string text)
    {
        textToSet.text = text;
    }

    private void ResetQueue()
    {
        textQueue.Clear();
        _nextTextIndex = 0;
    }

    private void Start()
    {
        GameState().dialogTextPanel = this;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click on dialog");

        Dialog(_nextTextIndex);
    }
}
