using UnityEngine;

public class DefaultSelectHandler : SelectHandler
{
    public override void StartBehavior()
    {
        ContextPanel.Instance.SetDefaultDisplay();
    }

    public override void StopBehavior()
    {
        
    }

    public override void UpdateBehavior()
    {
        
    }
}
