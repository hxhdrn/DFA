using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class TransitionButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;

    public void UpdateButton(DFATransition transition)
    {
        string transitionText = transition.Character + " → ";
        DFAState endState = transition.EndState;
        if (endState == null)
        {
            transitionText += "reject";
            button.onClick = null;
            button.interactable = false;

        }
        else
        {
            if (button.onClick != null) button.onClick.RemoveAllListeners();
            // TODO: set button to select transition
            button.interactable = true;
            if (endState.Label == null || endState.Label == "")
            {
                transitionText += "unlabeled";
            }
            else
            {
                transitionText += endState.Label;
            }
        }
        text.text = transitionText;
    }
}
