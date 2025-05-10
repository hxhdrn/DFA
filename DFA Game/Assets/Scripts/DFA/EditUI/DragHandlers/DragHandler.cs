using UnityEngine;

public abstract class DragHandler : MonoBehaviour
{
    public abstract void StartDrag();
    public abstract void UpdateDrag();
    public abstract void StopDrag();
}
