
public class Desk : Container
{

    protected override bool IsScrollValid(Scroll scroll)
    {
        return scroll != null && !scrolls.Contains(scroll) && scrolls.Count == 0;
    }

    protected override bool AddScroll(Scroll scroll)
    {
        if (!base.AddScroll(scroll)) return false;
        scroll.ShowCompleteSprite();
        return true;
    }
}
