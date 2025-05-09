using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class HoverManager : Singleton<HoverManager>
{
    public IHoverHandler CurrentItem { get; private set; }
    public bool OnItem { get => CurrentItem != null; }
    [SerializeField] private BackgroundHoverHandler backgroundHandler;

    private void Start()
    {
        if (CurrentItem == null) HoverOnItem(backgroundHandler);
    }

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
            HoverOnItem(backgroundHandler);
        }
    }

    private void Update()
    {
        if (CurrentItem == null)
        {
            // should never be reached!
            Debug.LogError("Hover handler item is null");
        }
        else
        {
            CurrentItem.UpdateHover();
        }
    }
}
