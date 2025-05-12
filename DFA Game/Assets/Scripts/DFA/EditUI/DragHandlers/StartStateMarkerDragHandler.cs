using UnityEngine;
using UnityEngine.InputSystem;

public class StartStateMarkerDragHandler : DragHandler
{
    private Vector2 offset;
    private Vector2 startPos;
    [SerializeField] private HoverDetector hoverDetector;
    [SerializeField] private DFAStartStateMarker marker;

    public override void StartBehavior()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        startPos = transform.position;
        offset = startPos - mousePos;
        hoverDetector.DisableHover();
    }

    public override void StopBehavior()
    {
        if (HoverManager.Instance.CurrentBehavior is StateHoverHandler stateHover)
        {
            marker.StartState = stateHover.State;
        }
        else
        {
            marker.UpdateMarkerPosition(marker.StartState);
        }

        hoverDetector.EnableHover();
    }

    public override void UpdateBehavior()
    {
        if (HoverManager.Instance.CurrentBehavior is StateHoverHandler stateHover)
        {
            marker.UpdateMarkerPosition(stateHover.State);
        }
        else
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
            transform.position = mousePos + offset;
        }
    }
}
