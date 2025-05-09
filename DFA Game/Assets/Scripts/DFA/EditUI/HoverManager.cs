using UnityEngine;

public class HoverManager : Singleton<HoverManager>
{
    public IHoverHandler CurrentItem { get; private set; }
    public bool OnItem { get => CurrentItem != null; }
    public Vector2 CurrentMousePos { get; private set; }

    public void HoverOnItem(IHoverHandler item)
    {
        if (CurrentItem != item)
        {
            CurrentItem?.StopHover();
            item.StartHover();
        }
        CurrentItem = item;
    }

    public void EndHoverOnItem(IHoverHandler item)
    {
        if (CurrentItem == item)
        {
            CurrentItem.StopHover();
            CurrentItem = null;
        }
    }

    private void Update()
    {
        if (CurrentItem != null)
        {
            CurrentItem.UpdateHover();
        }
    }
}
