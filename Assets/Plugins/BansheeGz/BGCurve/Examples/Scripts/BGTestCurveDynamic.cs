using UnityEngine;
using BansheeGz.BGSpline.Curve;

namespace BansheeGz.BGSpline.Example
{
    // This is old obsolete example class, left for compatibility only.
    // DO NOT USE IT AS AN EXAMPLE PLEASE
    // Use Cc components (BGCurveBaseMath -> BGCcMath, LineRenderer -> BGCcVisualizationLineRenderer)
    [RequireComponent(typeof (BGCurve))]
    [RequireComponent(typeof (LineRenderer))]
    public class BGTestCurveDynamic : MonoBehaviour
    {
        private const int TimeToMoveUp = 3;

        public GameObject ObjectToMove;

        private BGCurve curve;
        private BGCurveBaseMath curveBaseMath;

        private float started;
        private float ratio;
        private LineRenderer lineRenderer;


        // Use this for initialization
        private void Start()
        {
            curve = GetComponent<BGCurve>();
//            curve.TraceChanges = true;

            lineRenderer = GetComponent<LineRenderer>();

//            curveBaseMath = new BGCurveBaseMath(curve, true);
            curveBaseMath = new BGCurveBaseMath(curve);
            started = Time.time;

            ResetLineRenderer();
            curve.Changed += (sender, args) => ResetLineRenderer();
        }

        private void ResetLineRenderer()
        {
            const int points = 50;

            var positions = new Vector3[points];
            for (var i = 0; i < points; i++)
            {
                positions[i] = curveBaseMath.CalcPositionByDistanceRatio(((float) i/(points - 1)));
            }

#if UNITY_5_5
            lineRenderer.numPositions = points;
#elif UNITY_5_6_OR_NEWER
            lineRenderer.positionCount = points;
#else
            lineRenderer.SetVertexCount(points);
#endif

            lineRenderer.SetPositions(positions);
        }

        // Update is called once per frame
        private void Update()
        {
            transform.RotateAround(Vector3.zero, Vector3.up, 40*Time.deltaTime);

            ratio = (Time.time - started)/TimeToMoveUp;
            if (ratio >= 1)
            {
                started = Time.time;
                ratio = 0;
            }
            else
            {
                ObjectToMove.transform.position = curveBaseMath.CalcPositionByDistanceRatio(ratio);
            }
        }
    }
}