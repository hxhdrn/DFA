using UnityEngine;

public class LevelList : Singleton<LevelList>
{
    [SerializeField] private Transform levelButtonParent;
    [SerializeField] private LevelButton levelButtonPrefab;

    public void MakeLevelButton(string puzzleID)
    {
        LevelButton levelButton = Instantiate(levelButtonPrefab, levelButtonParent);
        levelButton.PuzzleID = puzzleID;
    }

    private void Start()
    {
        foreach (string puzzleID in PuzzleData.Instance.GetAllIDs())
        {
            MakeLevelButton(puzzleID);
        }
    }
}
