using UnityEngine;

public abstract class SelectHandler : MonoBehaviour, IBehaviorHandler
{
    public abstract void StartBehavior();
    public abstract void StopBehavior();
    public abstract void UpdateBehavior();
}
