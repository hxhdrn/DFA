using UnityEngine;
using UnityEngine.U2D;

public class DFAArrowline : MonoBehaviour
{
    [SerializeField] private SpriteShapeController shapeController;
    [SerializeField] private DFATransition transition;
    [SerializeField] private DFAArrowhead arrowhead;
    [SerializeField] private DFACharacter character;
    [SerializeField] private Vector2 defaultEndPos = Vector2.right * 1f;
    [SerializeField] private float defaultCurveAngle = 0f;

    public Vector2 SelfCurveDirection { get; private set; }

    public Vector2 EndPos { get; private set; }
    public float CurveAngle { get; private set; }

    public void UpdateStatePositions()
    {
        UpdateStatePositions(transition.EndState);
    }

    public void UpdateStatePositions(DFAState endState)
    {
        if (endState == null)
        {
            CurveAngle = defaultCurveAngle;
            EndPos = defaultEndPos;
            UpdateCurve();
        }
        else if (endState == transition.OriginState)
        {
            UpdateSelfCurve();
        }
        else
        {
            EndPos = endState.transform.position - transition.OriginState.transform.position;
            UpdateCurve();
        }
    }

    public void UpdateEndPosition(Vector2 endpoint)
    {
        EndPos = endpoint - (Vector2)transition.OriginState.transform.position;
        UpdateCurve(true);
    }

    private void Start()
    {
        CurveAngle = defaultCurveAngle;
        UpdateStatePositions();
    }

    private void UpdateSelfCurve()
    {
        Vector2 startTangent = Quaternion.Euler(0, 0, 20) * SelfCurveDirection.normalized;
        Vector2 endTangent = Quaternion.Euler(0, 0, -20) * SelfCurveDirection.normalized;

        Vector2 startPointPos = startTangent.normalized * DFAState.StateRadius;
        Vector2 endPointPos = endTangent.normalized * DFAState.StateRadius;

        arrowhead.UpdateArrowhead(endPointPos, -endTangent.normalized);

        shapeController.spline.SetPosition(0, startPointPos);
        shapeController.spline.SetPosition(1, endPointPos);

        shapeController.spline.SetRightTangent(0, startPointPos);
        shapeController.spline.SetLeftTangent(1, endPointPos);

        character.UpdatePosition();
    }

    private void UpdateCurve(bool endIsDirect = false)
    {
        Vector2 startTangent = Quaternion.Euler(0, 0, CurveAngle) * EndPos.normalized;
        Vector2 endTangent = Quaternion.Euler(0, 0, -CurveAngle) * -EndPos.normalized;

        Vector2 startPointPos = startTangent.normalized * DFAState.StateRadius;
        Vector2 endPointPos = endIsDirect ? EndPos : endTangent.normalized * DFAState.StateRadius + EndPos;

        endTangent = -Vector2.Reflect(startTangent, Vector2.Perpendicular(endPointPos - startPointPos).normalized);

        arrowhead.UpdateArrowhead(endPointPos, -endTangent.normalized);

        shapeController.spline.SetPosition(0, startPointPos);
        shapeController.spline.SetPosition(1, endPointPos);

        float newDistance = Vector2.Distance(startPointPos, endPointPos);
        Vector2 scaledStartTangent = .3f * newDistance * startTangent;
        shapeController.spline.SetRightTangent(0, scaledStartTangent);
        shapeController.spline.SetLeftTangent(1, .3f * newDistance * endTangent);

        character.UpdatePosition();
    }

    public Vector2 GetCharacterPosition()
    {
        Vector2 startPointPos = shapeController.spline.GetPosition(0);
        Vector2 endPointPos = shapeController.spline.GetPosition(1);
        Vector2 tangent = shapeController.spline.GetRightTangent(0);
        Vector2 startToEnd = endPointPos - startPointPos;
        Vector2 tangentInContext = Quaternion.FromToRotation(startToEnd.normalized, Vector2.right) * tangent;
        Vector2 halfway = startToEnd / 2;
        Vector2 perp = Vector2.Perpendicular(startToEnd).normalized;
        Vector2 scaledPerp = perp * tangentInContext.y;
        Vector2 offset = .2f * (tangentInContext.y < 0 ? -perp : perp);
        Vector2 worldPos = (Vector2)transition.OriginState.transform.position + startPointPos + halfway + scaledPerp * .75f + offset;
        return worldPos;
    }

    public void UpdateAngle(float angle)
    {
        CurveAngle = angle;
        UpdateCurve();
    }

    public void UpdateSelfCurveDirection(Vector2 direction)
    {
        SelfCurveDirection = direction;
        UpdateSelfCurve();
    }
}
