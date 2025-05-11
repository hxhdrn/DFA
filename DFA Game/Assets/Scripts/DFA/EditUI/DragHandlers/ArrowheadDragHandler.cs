using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowheadDragHandler : DragHandler
{
    [SerializeField] private DFAArrowline arrowline;
    [SerializeField] private HoverDetector hoverDetector;
    [SerializeField] private HoverDetector lineHoverDetector;
    [SerializeField] private DFATransition transition;

    public override void StartBehavior()
    {
        hoverDetector.DisableHover();
        lineHoverDetector.DisableHover();
    }

    public override void StopBehavior()
    {
        if (HoverManager.Instance.CurrentBehavior is StateHoverHandler stateHover)
        {
            transition.EndState = stateHover.State;
        }
        else {
            transition.EndState = null;
        }

        hoverDetector.EnableHover();
        lineHoverDetector.EnableHover();
        arrowline.UpdateStatePositions();
    }

    public override void UpdateBehavior()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        // Debug.Log("Dragging arrowhead at: " + mousePos);
        if (HoverManager.Instance.CurrentBehavior is StateHoverHandler stateHover)
        {
            if (stateHover.State == transition.OriginState)
            {
                arrowline.UpdateSelfCurveDirection(mousePos - (Vector2)transition.OriginState.transform.position);
            }
            else
            {
                arrowline.UpdateStatePositions(stateHover.State);
            }
        }
        else
        {
            arrowline.UpdateEndPosition(mousePos);
        }
    }
}
