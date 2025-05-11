using UnityEngine;

public abstract class DraggableHoverHandler : HoverHandler
{
    [SerializeField] protected DragHandler dragHandler;
    public virtual DragHandler DragHandler { get => dragHandler; }
}
