using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleData : PersistentSingleton<PuzzleData>
{
    [System.Serializable]
    public class Puzzle
    {
        public string id;
        public string title;
        public string description;
        public bool completed;
        public TestString[] testStrings;
        public string[] puzzleUnlocks;
        public bool unlocked;
        public Puzzle(string id, string title, string description, bool completed, TestString[] testStrings, string[] puzzleUnlocks, bool unlocked = false)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.completed = completed;
            this.testStrings = testStrings;
            this.unlocked = unlocked;
            this.puzzleUnlocks = puzzleUnlocks;
        }
    }

    public struct TestString
    {
        public string testString;
        public bool shouldAccept;
        public TestString(string testString, bool shouldAccept)
        {
            this.testString = testString;
            this.shouldAccept = shouldAccept;
        }
    }

    [SerializeField] private TextAsset dataCSV;
    [SerializeField] private string testStringsPath;
    private Dictionary<string, Puzzle> puzzles;
    private Puzzle[] puzzleVals;

    protected override void Awake()
    {
        base.Awake();
        LoadPuzzleData();
    }

    public void LoadPuzzleData()
    {
        puzzles = new Dictionary<string, Puzzle>();
        string[] lines = dataCSV.text.Split("\n");
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i])) return;
            string[] values = lines[i].Split(",");
            LoadPuzzle(values);
        }
        puzzleVals = puzzles.Values.ToArray();
    }

    private void LoadPuzzle(string[] puzzleLine)
    {
        string id = puzzleLine[0];
        string title = puzzleLine[1];
        string description = puzzleLine[2];
        bool.TryParse(puzzleLine[3], out bool  completed);
        TestString[] testStrings = GetTestStrings(id);
        string[] unlockIDs = puzzleLine.Length > 4 ? puzzleLine[4].Split(" ").Select(p => p.Trim()).ToArray() : new string[0];
        bool startPuzz = id == "aba";
        Puzzle puzzle = new(id, title, description, completed, testStrings, unlockIDs, startPuzz);
        puzzles.Add(id, puzzle);
    }

    private TestString[] GetTestStrings(string id)
    {
        TextAsset file = Resources.Load<TextAsset>(testStringsPath + "/" + id);
        if (file == null)
        {
            Debug.LogWarning("Puzzle id " + id + " does not have a test string file!");
            return new TestString[0];
        }
        string[] lines = file.text.Split("\n");
        List<TestString> testStrings = new();
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i])) return testStrings.ToArray();
            string[] line = lines[i].Split(",");
            TestString testString = GetTestString(line);
            testStrings.Add(testString);
        }
        return testStrings.ToArray();
    }

    private TestString GetTestString(string[] line)
    {
        string testString = line[0];
        bool.TryParse(line[1], out bool shouldAccept);
        return new TestString(testString, shouldAccept);
    }

    public Puzzle GetPuzzleData(string puzzleID)
    {
        return puzzles.GetValueOrDefault(puzzleID);
    }

    public string[] GetAllIDs()
    {
        return puzzles.Keys.ToArray();
    }

    public void CompletePuzzle(string puzzleID) {
        if (puzzles.TryGetValue(puzzleID, out Puzzle puzzle))
        {
            puzzle.completed = true;
            foreach (string id in puzzle.puzzleUnlocks)
            {
                if (puzzles.TryGetValue(id, out Puzzle unlock))
                {
                    unlock.unlocked = true;
                }
                else
                {
                    Debug.LogWarning("Puzzle to unlock not found");
                }
            }
        }
        else
        {
            Debug.LogWarning("Puzzle to complete not found");
        }
    }
}
