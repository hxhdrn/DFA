using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : Singleton<PuzzleManager>
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] Image completedIcon;
    private string puzzleID;
    public string PuzzleID
    { 
        get=>puzzleID; 
        set
        {
            puzzleID = value;
            puzzle = PuzzleData.Instance.GetPuzzleData(puzzleID);
            title.text = puzzle.title;
            description.text = puzzle.description;
            completed = puzzle.completed;
            completedIcon.gameObject.SetActive(completed);
        }
    }
    private PuzzleData.Puzzle puzzle;
    public PuzzleData.TestString[] TestStrings { get => puzzle.testStrings; }
    private bool completed;
    public bool Completed
    {
        get=>completed;
        set
        {
            completed = value;
            completedIcon.gameObject.SetActive(value);
            PuzzleData.Instance.CompletePuzzle(PuzzleID);
        }
    }

    public PuzzleData.TestString[] GetTestStrings()
    {
        return puzzle.testStrings;
    }

    private void Start()
    {
        PuzzleID = PuzzleSelectionManager.Instance.PuzzleID; // TODO: set to actual puzzle
    }
}
