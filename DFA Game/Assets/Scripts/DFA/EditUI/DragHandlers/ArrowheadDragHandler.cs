using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ArrowheadDragHandler is a behavior that handles dragging a transition's arrowhead
/// </summary>
public class ArrowheadDragHandler : DragHandler
{
    /// <summary>Arrowline associated with the arrowhead</summary>
    [SerializeField] private DFAArrowline arrowline;
    /// <summary>HoverDetector that detects a pointer hovering on the arrowhead</summary>
    [SerializeField] private HoverDetector hoverDetector;
    /// <summary>HoverDetector that detects a pointer hovering on the arrowline</summary>
    [SerializeField] private HoverDetector lineHoverDetector;
    /// <summary>Transition the arrowhead belongs to</summary>
    [SerializeField] private DFATransition transition;

    /// <summary>
    /// Disables hovering on the arrowhead and the arrowline
    /// </summary>
    public override void StartBehavior()
    {
        hoverDetector.DisableHover();
        lineHoverDetector.DisableHover();
    }

    /// <summary>
    /// Sets new end state of the transition
    /// Re-enabled hovering on the arrowhead and the arrowline
    /// </summary>
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
        // arrowline.UpdateStatePositions();
    }

    /// <summary>
    /// Updates the arrowhead and arrowline based on the current mouse position
    /// </summary>
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
