using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public void SetCharacterParent(DFACharacter character)
    {
        character.transform.SetParent(transform);
        character.transform.localScale = Vector3.one;
    }

    public void UpdateAllCharacters()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<DFACharacter>(out DFACharacter character)) {
                character.UpdatePosition();
            }
        }
    }

    public void TranslateAllCharacters(Vector2 translation)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<DFACharacter>(out DFACharacter character))
            {
                character.transform.Translate(translation);
            }
        }
    }
}
