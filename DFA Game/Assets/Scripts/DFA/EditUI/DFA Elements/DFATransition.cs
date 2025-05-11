using UnityEngine;

public class DFATransition : MonoBehaviour
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
            endState = value;
            arrowline.UpdateStatePositions();
        }
    }
    public string Character { get => character.Value; }
    private void Update()
    {
        if (OriginState.PositionTracker.PositionChanged || (EndState != null && EndState.PositionTracker.PositionChanged))
        {
            arrowline.UpdateStatePositions();
            Debug.Log("State positions changed");
        }
    }
}
