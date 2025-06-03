using System;
using System.Collections.Generic;
using Diagrams;
using UnityEngine;
using UnityEngine.Splines;

public class AnimatedDotAnimator : MonoBehaviour
{
    private SplineContainer _spline;
    public DotOnSpline dotPrfab;
    
    private bool _animated;
    private readonly List<DotOnSpline> _dots = new List<DotOnSpline>();
    private MeshRenderer _meshRenderer;
    private float _timer;
    public float Speed;
    public float DotSpacing;
    private int _dotCount;
    
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _spline = GetComponent<SplineContainer>();
    }

    void Start()
    {
        _dotCount = Mathf.FloorToInt(_spline.Spline.GetLength()/ DotSpacing);
        for (int i = 0; i < _dotCount; i++)
        {
            var dot = Instantiate(dotPrfab, transform);
            dot.SetSpline(_spline.Spline);
            dot.SetOffset(i/ (float)_dotCount);
            _dots.Add(dot);
        }

        SetAnimated(false);
    }

    void Update()
    {
        if(_animated)
        {
            _timer += Time.deltaTime * Speed;
            foreach (var dot in _dots)
            {
                dot.SetLerp(_timer);
            }
        }
    }

    public void SetAnimated(bool animated)
    {
        _animated = animated;
        _meshRenderer.enabled = !animated;
        foreach (var dot in _dots)
        {
            dot.gameObject.SetActive(animated);
        }
    }
}
