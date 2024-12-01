using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting;

public class Desk : Container
{
    [SerializeField] private GameObject scrollOnDesk;
    [SerializeField] private SpriteRenderer scrollOnDeskSpriteRenderer;

    #region Primary Functions
    protected override void Start()
    {
        base.Start();

        DisableHovering();
    }
    #endregion

    protected override bool IsScrollValid(Scroll scroll)
    {
        return scroll != null && !scrolls.Contains(scroll) && scrolls.Count == 0;
    }

    public override bool AddScroll(Scroll scroll)
    {
        if (!base.AddScroll(scroll)) return false;

        scrollOnDesk.SetActive(true);
        scroll.baseScale = scroll.transform.localScale;
        scroll.transform.SetParent(scrollOnDeskSpriteRenderer.transform.parent, false);
        scroll.transform.localScale = Vector3.one * 1.150722f;
        scroll.secondScale = scroll.transform.localScale;
        scroll.transform.localPosition = new Vector3(0, -10.58f, 2.734f);

        return true;
    }
}