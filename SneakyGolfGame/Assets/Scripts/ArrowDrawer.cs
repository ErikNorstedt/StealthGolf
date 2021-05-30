using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArrowDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer_;

    private void Awake()
    {
        lineRenderer_ = GetComponent<LineRenderer>();
    }

    public void RenderLine(Vector3 startPoint, Vector3 endPoint, float ballY)
    {
        lineRenderer_.positionCount = 2;
        Vector3[] points = new Vector3[2];
        startPoint.y = ballY;
        endPoint.y = ballY;
        if(Vector3.Distance(startPoint, endPoint) > 9)
        {
            var dir = (endPoint - startPoint).normalized;
            var tmp = startPoint + dir * 9;
            endPoint = tmp;
        }
        
        points[0] = startPoint;
        points[1] = endPoint;

        lineRenderer_.SetPositions(points);
    }

    public void EndLine()
    {
        lineRenderer_.positionCount = 0;
    }
}
