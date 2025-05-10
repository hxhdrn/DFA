using NUnit;
using System.Xml.Serialization;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.U2D;

public class DFAArrowline : MonoBehaviour
{
    [SerializeField] private SpriteShapeController shapeController;
    [SerializeField] private DFATransition transition;
    [SerializeField] private DFAArrowhead arrowhead;
    [SerializeField] private Vector2 defaultEndPos = Vector2.right * 1f;
    [SerializeField] private float defaultCurveAngle = 0f;

    public Vector2 EndPos { get; private set; }
    public float CurveAngle { get; private set; }

    public void UpdateStatePositions()
    {
        if (transition.EndState == null)
        {
            CurveAngle = defaultCurveAngle;
            EndPos = defaultEndPos;
            UpdateCurve();
        }
        else
        {
            EndPos = transition.EndState.transform.position - transition.OriginState.transform.position;
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
        shapeController.spline.SetRightTangent(0, .3f * newDistance * startTangent);
        shapeController.spline.SetLeftTangent(1, .3f * newDistance * endTangent);
    }

    public void UpdateAngle(float angle)
    {
        CurveAngle = angle;
        UpdateCurve();
    }
}
