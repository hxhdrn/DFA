using TMPro;
using UnityEngine;

public class DFACharacter : MonoBehaviour
{
    [SerializeField] private string value;
    public string Value { get=>value; private set=>this.value = value; }
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private DFAArrowline arrowline;

    private void Start()
    {
        CharacterManager.Instance.SetCharacterParent(this);
        textComponent.text = value;
    }

    public void UpdatePosition()
    {
        Vector2 pos = arrowline.GetCharacterPosition();
        transform.position = pos;
    }
}
