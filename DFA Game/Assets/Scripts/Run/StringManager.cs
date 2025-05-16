using TMPro;
using UnityEngine;

public class StringManager : Singleton<StringManager>
{
    [SerializeField] TextMeshPro textComponent;

    public void UpdateString()
    {
        string unprocessed = RunManager.Instance.RunningString;
        string result = "";
        int index = RunManager.Instance.PlaceInString;

        if (!string.IsNullOrEmpty(unprocessed))
        {
            result += "<i>";
            if (index >= unprocessed.Length)
            {
                result += unprocessed + "</i>";
            }
            else
            {
                result += unprocessed[..index];
                result += "</i>";
                result += "<b>" + unprocessed[index] + "</b>";
                if (index + 1 < unprocessed.Length)
                {
                    result += unprocessed[(index + 1)..];
                }
            }
        }

        textComponent.text = result;

        textComponent.transform.position = (Vector2)RunManager.Instance.CurrentState.transform.position + Vector2.down * (DFAState.StateRadius + .2f);
    }

    public void EnableString()
    {
        textComponent.gameObject.SetActive(true);
    }

    public void DisableString()
    {
        textComponent.gameObject.SetActive(false);
    }
}
