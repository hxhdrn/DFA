using UnityEngine;
using UnityEngine.InputSystem;

public class StateDragHandler : DragHandler
{
    private Vector2 offset;
    private Vector2 startPos;

    public override void StartDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        startPos = transform.position;
        offset = startPos - mousePos;
    }

    public override void StopDrag()
    {
        // potentially check conditions for ok position to drop
    }

    public override void UpdateDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        transform.position = mousePos + offset;
    }
}
