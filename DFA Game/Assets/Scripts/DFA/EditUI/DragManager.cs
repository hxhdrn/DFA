using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class DragManager : Singleton<DragManager>
{
    private IDragHandler dragHandler;
    private bool dragging;

    public void Drag(InputAction.CallbackContext cc)
    {
        if (cc.started)
        {
            // Get drag info but don't start in case it's just a click
            if (HoverManager.Instance.OnItem)
            {
                if (HoverManager.Instance.CurrentItem is IDraggableHoverHandler dragHover)
                {
                    dragHandler = dragHover.GetDragHandler();
                }
            }
            else
            {
                // instantiate selection box & get its drag handler
                dragHandler = null;
            }
        }
        else if (cc.performed)
        {
            // Start actual drag
            if (dragHandler != null)
            {
                dragging = true;
                dragHandler.StartDrag();
            }
        }
        else if (cc.canceled)
        {
            dragging = false;
            if (dragHandler != null)
            {
                dragHandler.StopDrag();
                dragHandler = null;
            }
        }
    }

    public void MouseMoved(InputAction.CallbackContext cc)
    {
        if (cc.performed && dragging)
        {
            dragHandler.UpdateDrag();
        }
    }
}
