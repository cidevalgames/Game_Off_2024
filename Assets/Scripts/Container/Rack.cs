using UnityEngine.EventSystems;
using UnityEngine;

public abstract class Rack : Container
{
    #region Event Systems
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        return;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (!IsHovering())
            return;

        Scroll holdingScroll = eventData.selectedObject.GetComponent<Scroll>();

        AddScroll(holdingScroll);
        DisableHovering();
    }

    #endregion
}
