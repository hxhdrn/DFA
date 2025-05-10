using UnityEngine;

public class DFAArrowhead : MonoBehaviour
{
    DFAArrowline arrowline;

    public void UpdateArrowhead(Vector2 position, Vector2 direction)
    {
        transform.localPosition = position;
        transform.rotation = Quaternion.FromToRotation(Vector2.right, direction);
    }
}
