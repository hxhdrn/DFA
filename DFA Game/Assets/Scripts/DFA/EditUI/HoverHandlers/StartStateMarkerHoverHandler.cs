using UnityEngine;

public class StartStateMarkerHoverHandler : DraggableHoverHandler
{
    [SerializeField] private DFAStartStateMarker startStateMarker;

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
