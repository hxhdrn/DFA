using TMPro;
using UnityEngine;

public class TransitionDisplay : ContextDisplay
{
    [SerializeField] private TextMeshProUGUI characterText;
    [SerializeField] private StateButton originStateButton;
    [SerializeField] private StateButton endStateButton;
    private DFATransition transition;
    
    public void UpdateDisplay(DFATransition transition)
    {
        this.transition = transition;
        characterText.text = "Character: " + transition.Character;
        originStateButton.UpdateButton(transition.OriginState);
        endStateButton.UpdateButton(transition.EndState);
    }
}
