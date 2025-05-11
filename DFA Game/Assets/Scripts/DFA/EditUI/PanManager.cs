using UnityEngine;
using UnityEngine.InputSystem;

public class PanManager : Singleton<PanManager>
{
    private bool panningWithMouse;
    private Vector2 startCameraPos;
    private Vector2 startMousePos;

    public void PanWithMouse(InputAction.CallbackContext cc)
    {
        if (cc.performed)
        {
            // Debug.Log("Started panning");
            panningWithMouse = true;
            startCameraPos = Camera.main.transform.position;
            startMousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
            HoverManager.Instance.DisableHoverBehavior();
            // Debug.Log("Starting mouse pos: " + startMousePos);
        }
        else if (cc.canceled)
        {
            // Debug.Log("Stopped panning");
            panningWithMouse = false;
            CharacterManager.Instance.UpdateAllCharacters();
            HoverManager.Instance.EnableHoverBehavior();
        }
    }

    public void MouseMoved(InputAction.CallbackContext cc)
    {
        if (cc.performed && panningWithMouse)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(cc.ReadValue<Vector2>());
            Vector2 mouseDelta = startMousePos - mousePos;
            Camera.main.transform.Translate(mouseDelta);
            CharacterManager.Instance.TranslateAllCharacters(-mouseDelta);
        }
    }
}
