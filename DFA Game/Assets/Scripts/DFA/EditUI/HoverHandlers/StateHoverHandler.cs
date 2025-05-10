using UnityEngine;

public class StateHoverHandler : DraggableHoverHandler
{
    [SerializeField] private DFAState state;
    public DFAState State { get => state; }

    public override void StartHover()
    {
        // Debug.Log("Started hovering on state");
    }

    public override void StopHover()
    {
        // Debug.Log("Stopped hovering on state");
    }

    public override void UpdateHover()
    {
        // Debug.Log("Currently hovering on state");
    }
}
