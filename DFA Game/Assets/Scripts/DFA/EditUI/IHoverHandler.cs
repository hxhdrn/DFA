using UnityEngine;
using UnityEngine.EventSystems;

public interface IHoverHandler : IPointerEnterHandler, IPointerExitHandler
{
    public void StartHover();
    public void StopHover();
    public void UpdateHover();
}
