using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RunSettings : Singleton<RunSettings>
{
    private string testString;
    public string TestString
    { 
        get => testString; 
        set
        {
            if (RunManager.Instance.IsRunning)
            {
                inputField.text = testString;
                return;
            }
            testString = value;
            RunManager.Instance.SetTestMode(testString, ShouldAccept);
        }
    }
    private bool shouldAccept;
    public bool ShouldAccept
    { 
        get => shouldAccept; 
        set
        {
            if (RunManager.Instance.IsRunning)
            {
                shouldAcceptToggle.isOn = shouldAccept;
                return;
            }
            shouldAccept = value;
            RunManager.Instance.SetTestMode(testString, ShouldAccept);
        }
    }
    [SerializeField] private GameObject testSettings;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Toggle shouldAcceptToggle;
    [SerializeField] private Toggle testModeToggle;
    public void UpdateMode(bool isTestMode)
    {
        if (RunManager.Instance.IsRunning) return;
        if (isTestMode)
        {
            RunManager.Instance.SetTestMode(TestString, ShouldAccept);
            testSettings.SetActive(true);
        }
        else
        {
            RunManager.Instance.SetSubmitMode();
            testSettings.SetActive(false);
        }
    }

    public void DisableAll()
    {
        inputField.interactable = false;
        shouldAcceptToggle.interactable = false;
        testModeToggle.interactable = false;
    }

    public void EnableAll()
    {
        inputField.interactable = true;
        shouldAcceptToggle.interactable = true;
        testModeToggle.interactable = true;
    }

    public void Start()
    {
        testString = inputField.text;
        shouldAccept = shouldAcceptToggle.isOn;
        testModeToggle.isOn = false;
        UpdateMode(false);
    }
}
