using UnityEngine;
using UnityEngine.EventSystems;

public abstract class HoverHandler : MonoBehaviour, IBehaviorHandler
{
    [SerializeField] protected SelectHandler selectHandler;
    public virtual SelectHandler SelectHandler { get => selectHandler; }

    public abstract void StartBehavior();
    public abstract void StopBehavior();
    public abstract void UpdateBehavior();
}
