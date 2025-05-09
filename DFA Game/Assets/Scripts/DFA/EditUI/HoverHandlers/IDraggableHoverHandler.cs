using UnityEngine;

public interface IDraggableHoverHandler : IHoverHandler
{
    public IDragHandler GetDragHandler();
}
