using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RunManager : Singleton<RunManager>
{
    public string RunningString { get; set; } = "";
    public bool IsRunning { get; private set; } = false;
    public float StepTime { get; set; } = .5f;
    public bool Paused { get; private set; }
    public DFAState CurrentState { get; private set; }

    private int placeInString = 0;
    private Coroutine runningCoroutine;

    [SerializeField] private Image playButtonIcon;
    [SerializeField] private Sprite playIcon;
    [SerializeField] private Sprite pauseIcon;
    [SerializeField] private Button playButton;
    [SerializeField] private Button stopButton;
    [SerializeField] private Button stepButton;

    public void FinishedRunning(bool accepted)
    {
        Debug.Log("Ended run: string " + accepted);

        ExitPlayMode();
    }

    public void EnterPlayMode()
    {
        IsRunning = true;
        placeInString = 0;
        CurrentState = DFAStartStateMarker.Instance.StartState;
        stopButton.interactable = true;
        stepButton.interactable = false;
    }

    private void ExitPlayMode()
    {
        IsRunning = false;
        playButtonIcon.sprite = playIcon;
        stopButton.interactable = false;
        stepButton.interactable = true;
        playButton.interactable = true;
    }

    private IEnumerator RunCoroutine()
    {
        yield return new WaitForSeconds(StepTime);
        if (Paused)
        {
            stepButton.interactable = true;
            playButtonIcon.sprite = playIcon;
            playButton.interactable = true;
        }
        else if (IsRunning)
        {
            Step();
            if (IsRunning)
            {
                runningCoroutine = StartCoroutine(RunCoroutine());
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

    private void Step()
    {
        if (placeInString < RunningString.Length)
        {
            DFATransition transition = CurrentState.GetTransition("" + RunningString[placeInString]);
            if (transition.EndState == null)
            {
                FinishedRunning(false);
            }
            else
            {
                CurrentState = transition.EndState;
                placeInString++;
            }
        }
        else
        {
            // reached end of string
            FinishedRunning(CurrentState.IsAccepting);
        }
    }
}
