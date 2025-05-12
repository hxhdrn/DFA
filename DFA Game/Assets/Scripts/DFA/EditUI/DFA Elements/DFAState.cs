using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DFAState : DFAElement
{
    public static float StateRadius { get; private set; } = .5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite acceptingSprite;
    [SerializeField] private TextMeshPro labelText;
    private string label;
    public string Label
    { 
        get => label;
        set
        {
            label = value;
            labelText.text = label;
        }
    }
    private bool isAccepting;
    public bool IsAccepting
    {
        get => isAccepting;
        set
        {
            isAccepting = value;
            if (value)
            {
                spriteRenderer.sprite = acceptingSprite;
            }
            else
            {
                spriteRenderer.sprite = defaultSprite;
            }
        }
    }

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
