using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RunManager : Singleton<RunManager>
{
    public string RunningString { get; set; } = "";
    public bool IsRunning { get; private set; } = false;
    public float StepTime { get; set; } = .5f;
    public float Paused { get; private set; }
    public DFAState CurrentState { get; private set; }

    private int placeInString = 0;
    private Coroutine runningCoroutine;

    [SerializeField] private Image playButtonIcon;
    [SerializeField] private Sprite playIcon;
    [SerializeField] private Sprite pauseIcon;
    [SerializeField] private Button stopButton;
    [SerializeField] private Button stepButton;

    public void RunDFA()
    {
        IsRunning = true;
        placeInString = 0;
        CurrentState = DFAStartStateMarker.Instance.StartState;
        playButtonIcon.sprite = pauseIcon;
        stopButton.interactable = true;
        stepButton.interactable = false;
        runningCoroutine = StartCoroutine(RunCoroutine());
    }

    public void FinishedRunning(bool accepted)
    {
        IsRunning = false;
        Debug.Log("Ended run: string " + accepted);

        playButtonIcon.sprite = playIcon;
        stopButton.interactable = false;
        stepButton.interactable = true;
    }

    private IEnumerator RunCoroutine()
    {
        yield return new WaitForSeconds(StepTime);
        if (placeInString < RunningString.Length)
        {
            bool continuing = Step();
            if (continuing)
            {
                placeInString++;
                runningCoroutine = StartCoroutine(RunCoroutine());
            }
            else
            {
                FinishedRunning(false);
            }
        }
        else
        {
            // reached end of string
            FinishedRunning(CurrentState.IsAccepting);
        }
    }

    private bool Step()
    {
        DFATransition transition = CurrentState.GetTransition("" + RunningString[placeInString]);

        if (transition.EndState == null)
        {
            return false;
        }

        CurrentState = transition.EndState;
        return true;
    }
}
