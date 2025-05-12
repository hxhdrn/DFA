using UnityEngine;

public class DFATransition : DFAElement
{
    [SerializeField] private DFAArrowline arrowline;
    [SerializeField] private DFAState originState;
    [SerializeField] private DFACharacter character;
    public DFAState OriginState {  get=>originState; private set=>originState = value; }
    [SerializeField] private DFAState endState;
    public DFAState EndState
    {
        get => endState;
        set
        {
            if (endState != null) endState.RemoveTransitionToward(this);
            endState = value;
            if (endState != null) endState.AddTransitionToward(this);
            arrowline.UpdateStatePositions();
        }
    }
    public string Character { get => character.Value; }

    private void Start()
    {
        if (endState != null)
        {
            endState.AddTransitionToward(this);
        }
    }

    private void Update()
    {
        if (OriginState.PositionTracker.PositionChanged || (EndState != null && EndState.PositionTracker.PositionChanged))
        {
            arrowline.UpdateStatePositions();
            // Debug.Log("State positions changed");
        }
    }
}
