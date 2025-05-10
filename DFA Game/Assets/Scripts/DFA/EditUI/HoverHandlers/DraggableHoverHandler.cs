using UnityEngine;

public abstract class DraggableHoverHandler : HoverHandler
{
    [SerializeField] private DragHandler dragHandler;
    public DragHandler DragHandler { get => dragHandler; }
}
