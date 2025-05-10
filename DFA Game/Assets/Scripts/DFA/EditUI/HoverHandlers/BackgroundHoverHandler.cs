using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BackgroundHoverHandler : HoverHandler
{
    [SerializeField] private GameObject stateHoverPrefab;
    private GameObject hoverState;

    public override void StartHover()
    {
        hoverState = Instantiate(stateHoverPrefab);
        Vector2 newPos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        hoverState.transform.position = newPos;
    }

    public override void StopHover()
    {
        Destroy(hoverState);
    }

    public override void UpdateHover()
    {
        Vector2 newPos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        hoverState.transform.position = newPos;
    }
}
