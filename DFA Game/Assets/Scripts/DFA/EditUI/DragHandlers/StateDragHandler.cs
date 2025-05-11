using UnityEngine;
using UnityEngine.InputSystem;

public class StateDragHandler : DragHandler
{
    private Vector2 offset;
    private Vector2 startPos;

    public override void StartBehavior()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        startPos = transform.position;
        offset = startPos - mousePos;
    }

    public override void StopBehavior()
    {
        // potentially check conditions for ok position to drop
    }

    public override void UpdateBehavior()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        transform.position = mousePos + offset;
    }
}
