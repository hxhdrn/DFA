using UnityEngine;

public class ContextPanel : Singleton<ContextPanel>
{
    [SerializeField] private StateDisplay stateDisplay;
    [SerializeField] private TransitionDisplay transitionDisplay;
    [SerializeField] private DefaultDisplay defaultDisplay;

    private ContextDisplay currentContext;

    private void Start()
    {
        SetDefaultDisplay();
    }

    public void SetStateDisplay(DFAState state)
    {
        stateDisplay.UpdateDisplay(state);
        SetDisplayActive(stateDisplay);
    }

    public void SetTransitionDisplay(DFATransition transition)
    {
        transitionDisplay.UpdateDisplay(transition);
        SetDisplayActive(transitionDisplay);
    }

    public void SetDefaultDisplay()
    {
        SetDisplayActive(defaultDisplay);
    }

    private void SetDisplayActive(ContextDisplay display)
    {
        if (currentContext != null) currentContext.gameObject.SetActive(false);
        currentContext = display;
        currentContext.gameObject.SetActive(true);
    }
}
