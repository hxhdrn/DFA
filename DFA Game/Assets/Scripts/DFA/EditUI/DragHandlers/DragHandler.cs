using UnityEngine;

public abstract class DragHandler : MonoBehaviour, IBehaviorHandler
{
    public abstract void StartBehavior();
    public abstract void UpdateBehavior();
    public abstract void StopBehavior();
}
