using UnityEngine;

public class TransitionSelectHandler : SelectHandler
{
    [SerializeField] private DFATransition transition;

    public override void StartBehavior()
    {
        ContextPanel.Instance.SetTransitionDisplay(transition);
    }

    public override void StopBehavior()
    {
        
    }

    public override void UpdateBehavior()
    {
        
    }
}
