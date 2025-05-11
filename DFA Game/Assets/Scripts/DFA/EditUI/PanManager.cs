using UnityEngine;
using UnityEngine.InputSystem;

public class PanManager : BehaviorManager<PanManager, PanHandler>
{
    [SerializeField] private PanHandler panHandler;
    
    public void SelectPanBehavior()
    {
        SelectBehavior(panHandler);
    }

    protected override void Update() { }
}
