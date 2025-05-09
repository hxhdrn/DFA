using UnityEngine;
using UnityEngine.InputSystem;

public class DragStateHandler : MonoBehaviour, IDragHandler
{
    private Vector2 offset;
    private Vector2 startPos;

    public void StartDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        startPos = transform.position;
        offset = startPos - mousePos;
    }

    public void StopDrag()
    {
        // potentially check conditions for ok position to drop
    }

    public void UpdateDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        transform.position = mousePos + offset;
    }
}
