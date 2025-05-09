using UnityEngine;
using UnityEngine.InputSystem;

public class PanManager : Singleton<PanManager>
{
    private bool panningWithMouse;
    private Vector2 startPos;
    private Vector2 prevMousePos;

    public void PanWithMouse(InputAction.CallbackContext cc)
    {
        if (cc.performed)
        {
            Debug.Log("Started panning");
            panningWithMouse = true;
            startPos = Camera.main.transform.position;
            prevMousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
            Debug.Log("Starting mouse pos: " + prevMousePos);
        }
        else if (cc.canceled)
        {
            Debug.Log("Stopped panning");
            panningWithMouse = false;
        }
    }

    public void MouseMoved(InputAction.CallbackContext cc)
    {
        if (cc.performed && panningWithMouse)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(cc.ReadValue<Vector2>());
            Vector2 mouseDelta = prevMousePos - mousePos;
            Camera.main.transform.Translate(mouseDelta);
        }
    }
}
