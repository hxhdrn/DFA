using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// LevelButton represents one level in the level selection list that players can click on to enter the level
/// </summary>
public class LevelButton : MonoBehaviour
{
    private string puzzleID;
    /// <summary> ID of the puzzle this LevelButton opens /// </summary>
    public string PuzzleID { get => puzzleID; set { puzzleID = value; UpdateButton(); } }
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image doneImage;

    /// <summary>Updates the button text and enables/disables the button based on if the puzzle is unlocked using the current PuzzleID</summary>
    public void UpdateButton()
    {
        PuzzleData.Puzzle puzzle = PuzzleData.Instance.GetPuzzleData(PuzzleID);
        buttonText.text = puzzle.title;
        button.interactable = puzzle.unlocked;
        doneImage.gameObject.SetActive(puzzle.completed);
    }

    /// <summary>Opens the puzzle level this button points to</summary>
    public void PlayPuzzle()
    {
        PuzzleSelectionManager.Instance.PuzzleID = PuzzleID;
        SceneManager.LoadScene("Puzzle");
    }
}
