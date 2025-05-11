using UnityEngine;

public class StateHoverHandler : DraggableHoverHandler
{
    [SerializeField] private DFAState state;
    public DFAState State { get => state; }

    public override void StartBehavior()
    {
        // Debug.Log("Started hovering on state");
    }

    public override void StopBehavior()
    {
        // Debug.Log("Stopped hovering on state");
    }

    public override void UpdateBehavior()
    {
        // Debug.Log("Currently hovering on state");
    }
}
