using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class DFAStartStateMarker : Singleton<DFAStartStateMarker>
{
    [SerializeField] private DFAState startState;
    public DFAState StartState
    {
        get => startState;
        set 
        { 
            startState = value;
            transform.SetParent(startState.transform);
            UpdateMarkerPosition(startState);
        }
    }

    public void UpdateMarkerPosition(DFAState state)
    {
        transform.position = (Vector2)state.transform.position + Vector2.left * DFAState.StateRadius;
    }
}
