using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowlineHoverHandler : MonoBehaviour, IDraggableHoverHandler
{
    [SerializeField] private ArrowlineDragHandler dragHandler;

    public IDragHandler GetDragHandler()
    {
        return dragHandler;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter != gameObject) return;
        HoverManager.Instance.HoverOnItem(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverManager.Instance.EndHoverOnItem(this);
    }

    public void StartHover()
    {
        
    }

    public void StopHover()
    {
        
    }

    public void UpdateHover()
    {
        
    }
}
