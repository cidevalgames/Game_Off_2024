using System;
using System.Collections.Generic;
using Assets.Scripts.User_Interface;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogTextPanel : DevObject, IPointerClickHandler
{

    [HideInInspector]
    public List<string> textQueue = new List<string>();

    private int _nextTextIndex = 0;
    private TextMeshProUGUI _textToSet;

    private void Awake()
    {
        _textToSet = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Show()
    {
        var image = GetComponent<Image>();
        var textMesh = GetComponentInChildren<TextMeshProUGUI>();

        if (image && textMesh)
        {
            image.enabled = true;
            textMesh.enabled = true;
        }
    }
    
    private void Hide()
    {
        var image = GetComponent<Image>();
        var textMesh = GetComponentInChildren<TextMeshProUGUI>();

        if (image && textMesh)
        {
            image.enabled = false;
            textMesh.enabled = false;
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
        _textToSet.text = text;
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
