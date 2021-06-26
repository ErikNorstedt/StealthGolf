using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleDrawer : MonoBehaviour
{
    private struct CircleProperties
    {
        public float Radius;
        public float LineWidth;
        public CircleProperties(float radius, float lineWidth)
        {
            Radius = radius;
            LineWidth = lineWidth;
        }
    }

    [SerializeField]
    private int _resolution = 20;
    [SerializeField]
    private float _lerpSpeed = 3f;

    private LineRenderer _lineRenderer = null;
    private CircleProperties _currentCircle = new CircleProperties(1f, 0.5f);
    private CircleProperties _targetCircle = new CircleProperties(1f, 0.5f);

    private Transform _transform = null;
    private Transform _followTarget = null;
    private Vector3 _followOffset = Vector3.zero;

    private void Awake()
    {
        _transform = transform;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (_followTarget)
        {
            transform.position = _followTarget.position + _followOffset + Vector3.up * (_currentCircle.LineWidth / 2f);
        }

        if (_lineRenderer.enabled)
        {
            if (_lineRenderer.positionCount != _resolution)
                _lineRenderer.positionCount = _resolution;

            float anglePerCount = 360f / ((float)_resolution);
            for (int i = 0; i < _resolution; i++)
            {
                float currentAngle = anglePerCount * (float)i;
                currentAngle *= Mathf.Deg2Rad;

                Vector3 unitVector = new Vector3(Mathf.Cos(currentAngle), 0f, Mathf.Sin(currentAngle));
                _lineRenderer.SetPosition(i, _transform.position + (unitVector * _currentCircle.Radius));
            }

            _lineRenderer.widthMultiplier = _currentCircle.LineWidth;

            CircleProperties updatedCircle = _currentCircle;
            updatedCircle.Radius = Mathf.Lerp(_currentCircle.Radius, _targetCircle.Radius, _lerpSpeed * Time.unscaledDeltaTime);
            updatedCircle.LineWidth = Mathf.Lerp(_currentCircle.LineWidth, _targetCircle.LineWidth, _lerpSpeed * Time.unscaledDeltaTime);

            _currentCircle = updatedCircle;
        }
    }

    public void SetFollowTarget(Transform target, Vector3 offset)
    {
        _followTarget = target;
        _followOffset = offset;
    }

    public void TurnOff()
    {
        _lineRenderer.enabled = false;
        _targetCircle = new CircleProperties(1f, 0.5f);
    }

    public void SetCircle(float radius, float lineWidth)
    {
        _lineRenderer.enabled = true;
        _targetCircle = new CircleProperties(radius, lineWidth);
    }
}
