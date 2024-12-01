using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Container : DevObject, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Parchemins présents dans le conteneur au lancement du jeu")]
    [SerializeField]
    protected List<Scroll> scrolls;

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

    protected virtual bool IsScrollValid(Scroll scroll)
    {
        return scroll != null && !scrolls.Contains(scroll);
    }

    public abstract void OnPointerEnter(PointerEventData eventData);

    public abstract void OnPointerExit(PointerEventData eventData);

    public abstract void OnPointerDown(PointerEventData eventData);

    public abstract void OnPointerUp(PointerEventData eventData);
}
