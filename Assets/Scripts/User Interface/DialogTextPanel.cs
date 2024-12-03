using System;
using System.Collections.Generic;
using Assets.Scripts.Desk.Objects;
using Assets.Scripts.User_Interface;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogTextPanel : DevObject, IPointerClickHandler
{

    [HideInInspector]
    public List<string> textQueue = new List<string>();
    [HideInInspector]
    public List<UnityEvent> eventQueue = new List<UnityEvent>();

    private int _nextTextIndex = 0;
    private TextMeshProUGUI _textToSet;

    private UnityEvent _onDialogEnd;

    private void Awake()
    {
        _textToSet = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Show()
    {
        var image = GetComponent<Image>();
        var textMesh = GetComponentInChildren<TextMeshProUGUI>();

        FindAnyObjectByType<MyCursor>().DisableCursor();

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

        FindAnyObjectByType<MyCursor>().EnableCursor();

        if (image && textMesh)
        {
            image.enabled = false;
            textMesh.enabled = false;
        }
    }

    public void Dialog(int index, UnityEvent onDialogEnd)
    {
        if (index > textQueue.Count - 1) // Il n'y a plus de dialogues à afficher
        {
            onDialogEnd.Invoke();

            _onDialogEnd = new UnityEvent();

            Hide();
            ResetQueue(); 
            //GameState().monk.Disappear();

            return;
        }

        if (index == 0)
        {
            eventQueue[index].Invoke();

            _onDialogEnd = onDialogEnd;

            _nextTextIndex = 1;
            SetText(textQueue[0]);
            Show();
            return;
        }

        eventQueue[index].Invoke();

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
        eventQueue.Clear();
        _nextTextIndex = 0;
    }

    public void EnableInteractableObjects()
    {
        foreach (var obj in FindObjectsByType<InteractableObject>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            obj.enabled = true;
        }
    }

    public void DisableInteractableObjects()
    {
        foreach (var obj in FindObjectsByType<InteractableObject>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            obj.enabled = false;
        }
    }

    public void EnableContainers()
    {
        foreach (var obj in FindObjectsByType<Container>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            obj.enabled = true;

            Debug.Log(obj.name);
        }
    }

    public void DisableContainers()
    {
        foreach (var obj in FindObjectsByType<Container>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            obj.enabled = false;
        }
    }

    private void Start()
    {
        GameState().dialogTextPanel = this;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Dialog(_nextTextIndex, _onDialogEnd);
    }
}
