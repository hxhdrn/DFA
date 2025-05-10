using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    private Vector2 lastPosition;
    public bool PositionChanged {  get; private set; }

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (lastPosition != (Vector2)transform.position)
        {
            PositionChanged = true;
        }
        else
        {
            PositionChanged = false;
        }
        lastPosition = transform.position;
    }
}
