using UnityEngine.EventSystems;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public class Shelf : Container
{
    #region Event Systems
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (scrolls.Count <= 0)
            return;

        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (scrolls.Count <= 0)
            return;

        base.OnPointerExit(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (scrolls.Count <= 0)
            return;

        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (scrolls.Count <= 0)
            return;

        base.OnPointerUp(eventData);
    }
    #endregion
}
