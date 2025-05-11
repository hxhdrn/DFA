using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public void SetCharacterParent(DFACharacter character)
    {
        character.transform.SetParent(transform);
        character.transform.localScale = Vector3.one;
    }
}
