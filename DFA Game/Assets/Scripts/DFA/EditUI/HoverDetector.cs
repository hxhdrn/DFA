using UnityEngine;
using UnityEngine.EventSystems;

public class HoverDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] HoverHandler hoverHandler;
    private int originalLayer;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != gameObject) return;
        HoverManager.Instance.SelectBehavior(hoverHandler);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverManager.Instance.DeselectBehavior(hoverHandler);
        if (eventData.pointerCurrentRaycast.gameObject != null && transform.IsChildOf(eventData.pointerCurrentRaycast.gameObject.transform))
        {
            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<HoverDetector>(out var parentHover))
            {
                parentHover.OnPointerEnter(eventData);
            }
        }
    }

    public void EnableHover()
    {
        gameObject.layer = originalLayer;
    }

    public void DisableHover()
    {
        originalLayer = gameObject.layer;
        gameObject.layer = 2;
    }
}
