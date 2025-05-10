using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class HoverManager : Singleton<HoverManager>
{
    public HoverHandler CurrentItem { get; private set; }
    public bool OnItem { get => CurrentItem != null; }
    [SerializeField] private BackgroundHoverHandler backgroundHandler;
    private bool hoverEnabled = true;

    private void Start()
    {
        if (CurrentItem == null) HoverOnItem(backgroundHandler);
    }

    public void HoverOnItem(HoverHandler item)
    {
        if (CurrentItem != item)
        {
            if (CurrentItem != null) CurrentItem.StopHover();
            if (hoverEnabled) item.StartHover();
        }
        CurrentItem = item;
    }

    public void EndHoverOnItem(HoverHandler item)
    {
        if (CurrentItem == item)
        {
            HoverOnItem(backgroundHandler);
        }
    }

    public void DisableHoverBehavior()
    {
        if (CurrentItem != null) CurrentItem.StopHover();
        hoverEnabled = false;
    }

    public void EnableHoverBehavior()
    {
        if (CurrentItem != null) CurrentItem.StartHover();
        hoverEnabled = true;
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
            if (hoverEnabled) CurrentItem.UpdateHover();
            Debug.Log("Hovering on " +  CurrentItem.name);
        }
    }
}
