using UnityEngine;
using UnityEngine.EventSystems;

public abstract class HoverHandler : MonoBehaviour
{
    public abstract void StartHover();
    public abstract void StopHover();
    public abstract void UpdateHover();
}
