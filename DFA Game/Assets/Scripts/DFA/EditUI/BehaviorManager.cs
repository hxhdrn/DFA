using UnityEngine;

public abstract class BehaviorManager<ManagerType, HandlerType> : Singleton<ManagerType> 
    where HandlerType : MonoBehaviour, IBehaviorHandler 
    where ManagerType : BehaviorManager<ManagerType, HandlerType>
{
    public HandlerType CurrentBehavior {  get; protected set; }
    public bool DefaultBehaviorSelected { get => CurrentBehavior == defaultBehavior; }
    [SerializeField] protected HandlerType defaultBehavior;
    public bool BehaviorEnabled { get; protected set; } = true;

    public bool BehaviorChangeEnabled { get; set; } = true;

    public void SelectBehavior(HandlerType item)
    {
        if (BehaviorChangeEnabled && CurrentBehavior != item)
        {
            if (CurrentBehavior != null) CurrentBehavior.StopBehavior();
            CurrentBehavior = item;
            if (BehaviorEnabled && CurrentBehavior != null) CurrentBehavior.StartBehavior();
        }
    }

    private void Start()
    {
        if (CurrentBehavior == null) SelectBehavior(defaultBehavior);
    }

    public void DeselectBehavior(HandlerType item)
    {
        if (BehaviorChangeEnabled && CurrentBehavior == item)
        {
            SelectBehavior(defaultBehavior);
        }
    }

    public void SelectDefaultBehavior()
    {
        SelectBehavior(defaultBehavior);
    }

    public void EnableBehavior()
    {
        if (!BehaviorEnabled)
        {
            BehaviorEnabled = true;
            if (CurrentBehavior != null) CurrentBehavior.StartBehavior();
        }
    }

    public void DisableBehavior()
    {
        if (BehaviorEnabled)
        {
            BehaviorEnabled = false;
            if (CurrentBehavior != null) CurrentBehavior.StopBehavior();
        }
    }

    public void UpdateBehavior()
    {
        if (BehaviorEnabled)
        {
            if (CurrentBehavior != null) CurrentBehavior.UpdateBehavior();
        }
    }

    protected virtual void Update()
    {
        UpdateBehavior();
    }
}
