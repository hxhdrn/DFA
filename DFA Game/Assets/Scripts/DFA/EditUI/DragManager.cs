using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class DragManager : Singleton<DragManager>
{
    [SerializeField] float startDragDistance = 5f;

    private IDragHandler dragHandler;
    private bool dragging;
    private bool tapStillAllowed;

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
            tapStillAllowed = true;
        }
        if (cc.performed)
        {
            if (!dragging)
            {
                StartDrag();
            }
        }
        else if (cc.canceled)
        {
            if (dragging)
            {
                dragging = false;
                if (dragHandler != null)
                {
                    dragHandler.StopDrag();
                    dragHandler = null;
                }
            }
            else if (tapStillAllowed)
            {
                if (HoverManager.Instance.CurrentItem is BackgroundHoverHandler)
                {
                    StateCreator.Instance.CreateState();
                }
            }
        }
    }

    private void StartDrag()
    {
        tapStillAllowed = false;
        // Start actual drag
        if (dragHandler != null)
        {
            dragging = true;
            dragHandler.StartDrag();
        }
    }

    public void MouseMoved(InputAction.CallbackContext cc)
    {
        if (cc.performed)
        {
            if (dragging)
            {
                dragHandler.UpdateDrag();
            }
            else if (Pointer.current.delta.ReadValue().magnitude >= startDragDistance)
            {
                StartDrag();
            }
        }
    }
}
