using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateDisplay : ContextDisplay
{
    [SerializeField] TMP_InputField label;
    [SerializeField] Toggle accepting;
    [SerializeField] TransitionButton transitionButtonA;
    [SerializeField] TransitionButton transitionButtonB;

    private DFAState currentState;

    public void UpdateDisplay(DFAState state)
    {
        currentState = state;
        label.text = state.Label;
        accepting.isOn = state.IsAccepting;
        transitionButtonA.UpdateButton(currentState.GetTransition("a"));
        transitionButtonB.UpdateButton(currentState.GetTransition("b"));
    }

    public void UpdateLabel(string value)
    {
        currentState.Label = value;
        // TODO: update visual label on state
    }

    public void UpdateAccepting(bool value)
    {
        currentState.IsAccepting = value;
        // TODO: update state visual
    }
}
