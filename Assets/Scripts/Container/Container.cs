using System.Collections.Generic;
using UnityEngine;

public abstract class Container : DevObject
{
    [SerializeField]
    [Header("Parchemins présents dans le conteneur au lancement du jeu")]
    protected List<Scroll> scrolls;

    protected void TransferScrollTo(Scroll scroll, Container transferTarget)
    {
        
        if (!transferTarget.AddScroll(scroll)) return;
        
        scrolls.Remove(scroll);
        
    }

    protected virtual bool AddScroll(Scroll scroll)
    {
        if (!IsScrollValid(scroll)) return false;
        scrolls.Add(scroll);
        return true;
    }

    protected virtual bool IsScrollValid(Scroll scroll)
    {
        return scroll != null && !scrolls.Contains(scroll);
    }
}
