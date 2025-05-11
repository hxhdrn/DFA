using TMPro;
using UnityEngine;

public class DFACharacter : MonoBehaviour
{
    [SerializeField] private string value;
    public string Value { get=>value; private set=>this.value = value; }
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private DFATransition transition;

    private void Start()
    {
        CharacterManager.Instance.SetCharacterParent(this);
        textComponent.text = value;
    }

    public void UpdatePosition(Vector2 startPointPos, Vector2 endPointPos, Vector2 startTangent)
    {
        Vector2 startToEnd = endPointPos - startPointPos;
        Vector2 tangentInContext = Quaternion.FromToRotation(startToEnd.normalized, Vector2.right) * startTangent;
        Debug.Log("Tangent in context: " + tangentInContext.normalized);
        Vector2 halfway = startToEnd / 2;
        Vector2 perp = Vector2.Perpendicular(startToEnd).normalized;
        Vector2 scaledPerp = perp * tangentInContext.y;
        Vector2 offset = .2f * (tangentInContext.y < 0 ? -perp : perp);
        Vector2 worldPos = (Vector2)transition.OriginState.transform.position + startPointPos + halfway + scaledPerp * .75f + offset;
        transform.position = worldPos;
    }
}
