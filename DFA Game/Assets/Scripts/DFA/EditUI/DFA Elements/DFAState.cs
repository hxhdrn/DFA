using System.Collections.Generic;
using UnityEngine;

public class DFAState : DFAElement
{
    public static float StateRadius { get; private set; } = .5f;

    public string Label { get; set; }
    public bool IsAccepting { get; set; }

    [SerializeField] private PositionTracker positionTracker;
    public PositionTracker PositionTracker { get => positionTracker; }

    [SerializeField] private DFATransition[] transitionInput;

    private Dictionary<string, DFATransition> transitions;

    private void Start()
    {
        transitions = new Dictionary<string, DFATransition>();
        foreach (DFATransition t in transitionInput)
        {
            transitions.Add(t.Character, t);
        }
    }

    public DFATransition GetTransition(string character)
    {
        return transitions[character];
    }
}
