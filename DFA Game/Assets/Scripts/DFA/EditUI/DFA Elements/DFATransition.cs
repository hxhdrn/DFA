using UnityEngine;

public class DFATransition : MonoBehaviour
{
    [SerializeField] DFAArrowline arrowline;
    [SerializeField] private DFAState originState;
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
    [SerializeField] private string character = "x";
    public string Character { get=>character; private set=>character = value; }
    private void Update()
    {
        if (OriginState.PositionTracker.PositionChanged || (EndState != null && EndState.PositionTracker.PositionChanged))
        {
            arrowline.UpdateStatePositions();
            Debug.Log("State positions changed");
        }
    }
}
