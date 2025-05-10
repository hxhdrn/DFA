using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowlineDragHandler : DragHandler
{
    [SerializeField] private DFAArrowline arrowline;
    [SerializeField] private DFATransition transition;

    private float startAngle;

    public override void StartDrag()
    {
        startAngle = arrowline.CurveAngle;
    }

    public override void StopDrag()
    {
        
    }

    public override void UpdateDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        DFAState closerState = transition.OriginState;
        if (Vector2.Distance(closerState.transform.position, mousePos) > Vector2.Distance(transition.EndState.transform.position, mousePos))
        {
            closerState = transition.EndState;
        }

        Vector2 toMouse = mousePos - (Vector2)closerState.transform.position;
        float newAngle = Vector2.SignedAngle(arrowline.EndPos.normalized, toMouse.normalized);
        if (closerState == transition.EndState)
        {
            newAngle = -Vector2.SignedAngle(-arrowline.EndPos.normalized, toMouse.normalized);
        }
        newAngle = Mathf.Clamp(newAngle, -90, 90);
        arrowline.UpdateAngle(newAngle);
    }
}
