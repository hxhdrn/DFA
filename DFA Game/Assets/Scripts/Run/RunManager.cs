using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RunManager : Singleton<RunManager>
{
    public string RunningString { get; set; } = "";
    public bool IsRunning { get; private set; } = false;
    public float StepTime { get; set; } = 2f;
    public bool Paused { get; private set; }
    public DFAState CurrentState { get; private set; }

    public int PlaceInString { get; private set; } = 0;
    private Coroutine runningCoroutine;
    public PuzzleData.TestString[] TestList { get; private set; }
    private int placeInList;
    public bool InTestMode { get; private set; }
    private bool allPassed = true;
    private bool gotResult = false;

    [SerializeField] private Image playButtonIcon;
    [SerializeField] private Sprite playIcon;
    [SerializeField] private Sprite pauseIcon;
    [SerializeField] private Button playButton;
    [SerializeField] private Button stopButton;
    [SerializeField] private Button stepButton;

    public void FinishedRunning(bool accepted)
    {
        Debug.Log("place in list " + placeInList);
        bool result = accepted == TestList[placeInList].shouldAccept;
        allPassed &= result;
        ResultManager.Instance.UpdateResult(accepted, RunningString, TestList[placeInList].shouldAccept);
        Debug.Log("Ended run:" + RunningString + (accepted ? " accepted" : " rejected") + " (" + (result ? "passed" : "failed") + ")");

        placeInList++;
        if (IsRunning && placeInList < TestList.Length)
        {
            ResetPlayMode();
        }
        else
        {
            if (!InTestMode)
            {
                ResultManager.Instance.UpdateTotalResult(allPassed);
                if (allPassed)
                {
                    PuzzleManager.Instance.Completed = true;
                }
            }
            ExitPlayMode();
        }
    }

    public void DisplayResult()
    {

    }

    public void SetTestMode(string testString, bool shouldBeAccepted)
    {
        InTestMode = true;
        TestList = new PuzzleData.TestString[] { new(testString, shouldBeAccepted) };
    }

    public void SetSubmitMode()
    {
        InTestMode = false;
        TestList = PuzzleManager.Instance.GetTestStrings();
    }

    public void EnterPlayMode()
    {
        IsRunning = true;
        allPassed = true;
        placeInList = 0;
        StepTime = 1.5f;
        RunSettings.Instance.DisableAll();
        ResetPlayMode();
    }

    private void ResetPlayMode()
    {
        RunningString = TestList[placeInList].testString;
        Debug.Log("Testing " + RunningString);
        PlaceInString = 0;
        CurrentState = DFAStartStateMarker.Instance.StartState;
        StringManager.Instance.UpdateString();
        StringManager.Instance.EnableString();
        stopButton.interactable = true;
        stepButton.interactable = false;
    }

    private void ExitPlayMode()
    {
        IsRunning = false;
        playButtonIcon.sprite = playIcon;
        StringManager.Instance.DisableString();
        stopButton.interactable = false;
        stepButton.interactable = true;
        playButton.interactable = true;
        RunSettings.Instance.EnableAll();
    }

    private IEnumerator RunCoroutine()
    {
        while (IsRunning)
        {
            yield return new WaitForSeconds(StepTime);
            if (Paused)
            {
                stepButton.interactable = true;
                playButtonIcon.sprite = playIcon;
                playButton.interactable = true;
            }
            else
            {
                Step();
            }
        }
    }

    public void TogglePause()
    {
        if (!IsRunning)
        {
            EnterPlayMode();
            PlayDFA();
        }
        else if (Paused)
        {
            PlayDFA();
        }
        else
        {
            PauseDFA();
        }
    }

    private IEnumerator StepCoroutine()
    {
        yield return new WaitForSeconds(StepTime);
        if (IsRunning)
        {
            Step();
            FinishStep();
        }
    }

    public void StepDFA()
    {
        if (!IsRunning)
        {
            EnterPlayMode();
        }
        stepButton.interactable = false;
        playButton.interactable = false;
        runningCoroutine = StartCoroutine(StepCoroutine());
    }

    private void FinishStep()
    {
        if (IsRunning)
        {
            stepButton.interactable = true;
            playButton.interactable = true;
        }
    }

    public void StopDFA()
    {
        ExitPlayMode();
    }

    public void PauseDFA()
    {
        playButton.interactable = false;
        Paused = true;
    }

    public void PlayDFA()
    {
        Paused = false;
        playButtonIcon.sprite = pauseIcon;
        stepButton.interactable = false;
        stopButton.interactable = true;
        runningCoroutine = StartCoroutine(RunCoroutine());
    }

    public void SkipDFA()
    {
        if (!IsRunning)
        {
            EnterPlayMode();
        }
        PauseDFA();
        while (IsRunning)
        {
            Step();
        }
    }

    private void Step()
    {
        if (PlaceInString < RunningString.Length)
        {
            Debug.Log("Stepping at " + PlaceInString + ": " + RunningString[PlaceInString]);
            DFATransition transition = CurrentState.GetTransition("" + RunningString[PlaceInString]);

            if (transition.EndState == null)
            {
                FinishedRunning(false);
            }
            else
            {
                CurrentState = transition.EndState;
                PlaceInString++;
                StringManager.Instance.UpdateString();
            }
        }
        else
        {
            // reached end of string
            FinishedRunning(CurrentState.IsAccepting);
        }
    }
}
