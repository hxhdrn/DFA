using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowlineHoverHandler : DraggableHoverHandler
{
    [SerializeField] private ArrowheadDragHandler arrowheadDragHandler;
    [SerializeField] private DFATransition transition;
    public override DragHandler DragHandler => transition.EndState == null ? arrowheadDragHandler : dragHandler;

    public override void StartBehavior()
    {
        
    }

    public override void StopBehavior()
    {
        
    }

    public override void UpdateBehavior()
    {
        
    }
}
