using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BackgroundHoverHandler : MonoBehaviour, IHoverHandler
{
    [SerializeField] private GameObject stateHoverPrefab;
    private GameObject hoverState;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.LogError("Pointer cannot enter background");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.LogError("Pointer cannot exit background");
    }

    public void StartHover()
    {
        hoverState = Instantiate(stateHoverPrefab);
        Vector2 newPos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        hoverState.transform.position = newPos;
    }

    public void StopHover()
    {
        Destroy(hoverState);
    }

    public void UpdateHover()
    {
        Vector2 newPos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        hoverState.transform.position = newPos;
    }
}
