using UnityEngine;

public class StateHoverHandler : MonoBehaviour, IDraggableHoverHandler
{
    [SerializeField] private DFAState state;
    [SerializeField] private StateDragHandler dragStateHandler;

    public IDragHandler GetDragHandler()
    {
        return dragStateHandler;
    }

    public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        HoverManager.Instance.HoverOnItem(this);
    }

    public void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        HoverManager.Instance.EndHoverOnItem(this);
    }

    public void StartHover()
    {
        Debug.Log("Started hovering on state");
    }

    public void StopHover()
    {
        Debug.Log("Stopped hovering on state");
    }

    public void UpdateHover()
    {
        Debug.Log("Currently hovering on state");
    }
}
