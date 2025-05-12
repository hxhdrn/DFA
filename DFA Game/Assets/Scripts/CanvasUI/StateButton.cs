using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class StateButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;

    public void UpdateButton(DFAState state)
    {
        if (state == null)
        {
            text.text = "reject";
        }
        else if (state.Label == null || state.Label == "")
        {
            text.text = "unlabeled";
        }
        else
        {
            text.text = state.Label;
        }

        // TODO: make button select state
    }
}
