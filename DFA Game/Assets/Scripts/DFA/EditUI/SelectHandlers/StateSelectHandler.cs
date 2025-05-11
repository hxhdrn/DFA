using UnityEngine;

public class StateSelectHandler : SelectHandler
{
    [SerializeField] private DFAState state;

    public override void StartBehavior()
    {
        ContextPanel.Instance.SetStateDisplay(state);
    }

    public override void StopBehavior()
    {
        
    }

    public override void UpdateBehavior()
    {
        
    }
}
