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
    private HashSet<DFATransition> transitionsToward;

    private void Start()
    {
        transitions = new Dictionary<string, DFATransition>();
        foreach (DFATransition t in transitionInput)
        {
            transitions.Add(t.Character, t);
        }
        transitionsToward = new HashSet<DFATransition>();
    }

    public DFATransition GetTransition(string character)
    {
        return transitions[character];
    }

    public void AddTransitionToward(DFATransition transition)
    {
        transitionsToward.Add(transition);
    }

    public void RemoveTransitionToward(DFATransition transition)
    {
        transitionsToward.Remove(transition);
    }

    public void DeleteState()
    {
        foreach (DFATransition t in transitionsToward)
        {
            t.EndState = null;
        }
        Destroy(gameObject);
    }
}
