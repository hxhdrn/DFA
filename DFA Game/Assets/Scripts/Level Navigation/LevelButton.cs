using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    private string puzzleID;
    public string PuzzleID { get => puzzleID; set { puzzleID = value; UpdateButton(); } }
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image doneImage;
    
    public void UpdateButton()
    {
        PuzzleData.Puzzle puzzle = PuzzleData.Instance.GetPuzzleData(PuzzleID);
        buttonText.text = puzzle.title;
        button.interactable = puzzle.unlocked;
        doneImage.gameObject.SetActive(puzzle.completed);
    }

    public void PlayPuzzle()
    {
        PuzzleSelectionManager.Instance.PuzzleID = PuzzleID;
        SceneManager.LoadScene("Puzzle");
    }
}
