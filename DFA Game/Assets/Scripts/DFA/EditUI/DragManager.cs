using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class DragManager : Singleton<DragManager>
{
    [SerializeField] float startDragDistance = 5f;

    private DragHandler dragHandler;
    public bool Dragging {get; private set;}
    private bool tapStillAllowed;

    public void Drag(InputAction.CallbackContext cc)
    {
        if (cc.started)
        {
            // Get drag info but don't start in case it's just a click
            if (HoverManager.Instance.OnItem)
            {
                if (HoverManager.Instance.CurrentItem is DraggableHoverHandler dragHover)
                {
                    dragHandler = dragHover.DragHandler;
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
            if (!Dragging)
            {
                StartDrag();
            }
        }
        else if (cc.canceled)
        {
            if (Dragging)
            {
                EndDrag();
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
            Dragging = true;
            dragHandler.StartDrag();
            HoverManager.Instance.DisableHoverBehavior();
        }
    }

    private void EndDrag()
    {
        Dragging = false;
        if (dragHandler != null)
        {
            dragHandler.StopDrag();
            dragHandler = null;
            HoverManager.Instance.EnableHoverBehavior();
        }
    }

    public void MouseMoved(InputAction.CallbackContext cc)
    {
        if (cc.performed)
        {
            if (Dragging)
            {
                // Debug.Log("Dragging " + ((MonoBehaviour)dragHandler).name);
                dragHandler.UpdateDrag();
            }
            else if (Pointer.current.delta.ReadValue().magnitude >= startDragDistance)
            {
                StartDrag();
            }
        }
    }
}
