using UnityEngine;
using UnityEngine.InputSystem;

public class ClickManager : MonoBehaviour
{
    private bool startedClickInteraction;
    private bool startedRightClickInteraction;
    [SerializeField] float startDragDistance = 5f;
    private DragHandler dragHandler;
    public void Click(InputAction.CallbackContext cc)
    {
        if (cc.started)
        {
            // Get drag info but don't start in case it's just a click
            if (!HoverManager.Instance.DefaultBehaviorSelected && DragManager.Instance.BehaviorChangeEnabled)
            {
                if (HoverManager.Instance.CurrentBehavior is DraggableHoverHandler dragHover)
                {
                    dragHandler = dragHover.DragHandler;
                }
            }
            else
            {
                // instantiate selection box & get its drag handler
                dragHandler = null;
            }
            startedClickInteraction = true;
        }
        if (cc.performed)
        {
            StartDrag();
        }
        else if (cc.canceled)
        {
            if (!DragManager.Instance.DefaultBehaviorSelected)
            {
                EndDrag();
            }
            else if (startedClickInteraction)
            {
                if (HoverManager.Instance.DefaultBehaviorSelected && HoverManager.Instance.BehaviorEnabled)
                {
                    DFAState newState = StateCreator.Instance.CreateState();
                }
                else if (HoverManager.Instance.CurrentBehavior != null && HoverManager.Instance.BehaviorEnabled)
                {
                    SelectHandler newSelection = HoverManager.Instance.CurrentBehavior.SelectHandler;
                    if (SelectionManager.Instance.CurrentBehavior == newSelection)
                    {
                        SelectionManager.Instance.DeselectBehavior(newSelection);
                    }
                    else
                    {
                        SelectionManager.Instance.SelectBehavior(HoverManager.Instance.CurrentBehavior.SelectHandler);
                    }
                }
                startedClickInteraction = false;
            }
        }
    }

    public void RightClick(InputAction.CallbackContext cc)
    {
        if (cc.started)
        {
            startedRightClickInteraction = true;
        }
        else if (cc.performed)
        {
            StartPan();
        }
        else if (cc.canceled)
        {
            if (!PanManager.Instance.DefaultBehaviorSelected)
            {
                EndPan();
            }
            else if (startedRightClickInteraction)
            {
                if (HoverManager.Instance.CurrentBehavior is StateHoverHandler stateHover)
                {
                    Destroy(stateHover.State.gameObject);
                }
            }
        }
    }

    private void StartPan()
    {
        startedRightClickInteraction = false;
        if (PanManager.Instance.BehaviorChangeEnabled)
        {
            PanManager.Instance.SelectPanBehavior();
            HoverManager.Instance.DisableBehavior();
            DragManager.Instance.DisableBehavior();
            DragManager.Instance.BehaviorChangeEnabled = false;
        }
    }

    private void EndPan()
    {
        if (PanManager.Instance.BehaviorChangeEnabled)
        {
            PanManager.Instance.SelectDefaultBehavior();
            HoverManager.Instance.EnableBehavior();
            DragManager.Instance.EnableBehavior();
            DragManager.Instance.BehaviorChangeEnabled = true;
        }
    }

    private void StartDrag()
    {
        startedClickInteraction = false;
        // Start actual drag
        if (dragHandler != null && DragManager.Instance.BehaviorChangeEnabled)
        {
            DragManager.Instance.SelectBehavior(dragHandler);
            HoverManager.Instance.DisableBehavior();
            PanManager.Instance.DisableBehavior();
            PanManager.Instance.BehaviorChangeEnabled = false;
        }
    }

    private void EndDrag()
    {
        if (DragManager.Instance.BehaviorChangeEnabled)
        {
            DragManager.Instance.SelectDefaultBehavior();
            HoverManager.Instance.EnableBehavior();
            PanManager.Instance.EnableBehavior();
            PanManager.Instance.BehaviorChangeEnabled = true;
        }
    }

    public void MouseMoved(InputAction.CallbackContext cc)
    {
        if (cc.performed)
        {
            if (startedClickInteraction && Pointer.current.delta.ReadValue().magnitude >= startDragDistance)
            {
                StartDrag();
            }
            DragManager.Instance.UpdateBehavior();
            PanManager.Instance.UpdateBehavior();
        }
    }
}
