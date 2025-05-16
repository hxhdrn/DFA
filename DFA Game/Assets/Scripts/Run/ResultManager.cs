using System.Collections;
using TMPro;
using UnityEngine;

public class ResultManager : Singleton<ResultManager>
{
    [SerializeField] private TextMeshProUGUI resultText;
    private Coroutine coroutine;

    public void UpdateResult(bool accepted, string testString, bool shouldAccept)
    {
        string result = "";

        result += (accepted == shouldAccept) ? "<color=green>Passed: " : "<color=red>Failed: ";
        result += accepted ? "accepted " : "rejected ";
        result += (testString == string.Empty) ? "ϵ" : testString;

        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(ShowResult(result));
    }

    public void UpdateTotalResult(bool allPassed)
    {
        string result = "";
        if (allPassed)
        {
            result = "<color=green>Congrats, all test strings passed!";
        }
        else
        {
            result = "<color=red>Not all test strings passed, try something else.";
        }

        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(ShowResult(result));
    }

    private IEnumerator ShowResult(string result)
    {
        resultText.gameObject.SetActive(true);
        resultText.text = result;
        yield return new WaitForSeconds(3f);
        resultText.gameObject.SetActive(false);
    }
}
