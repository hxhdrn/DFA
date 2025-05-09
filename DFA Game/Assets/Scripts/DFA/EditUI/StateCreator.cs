using UnityEngine;
using UnityEngine.InputSystem;

public class StateCreator : Singleton<StateCreator>
{
    [SerializeField] private DFAState statePrefab;
    [SerializeField] private Transform dfaParent;

    public void CreateState()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        DFAState state = Instantiate(statePrefab, mousePos, Quaternion.identity, dfaParent);
    }
}
