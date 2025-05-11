using UnityEngine;
using UnityEngine.InputSystem;

public class PanHandler : MonoBehaviour, IBehaviorHandler
{
    private Vector2 startMousePos;

    public void StartBehavior()
    {
        startMousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
    }

    public void StopBehavior()
    {

    }

    public void UpdateBehavior()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        Vector2 mouseDelta = startMousePos - mousePos;
        if (mouseDelta != Vector2.zero)
        {
            Camera.main.transform.Translate(mouseDelta);
            CharacterManager.Instance.TranslateAllCharacters(-mouseDelta);
        }
    }
}
