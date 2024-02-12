using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine.Utility;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class OnScreenJoystick : MonoBehaviour
{
    [SerializeField] float _stickRange = 70;
    [SerializeField] RectTransform _minPoint;
    [SerializeField] RectTransform _maxPoint;
    [SerializeField] Vector2 _minPointPos;
    [SerializeField] Vector2 _maxPointPos;
    [SerializeField] Vector2 _initialPos;
    private RectTransform _transform;
    private bool _isControllerTouched = false;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
        _initialPos = _transform.position;
        _minPointPos = _minPoint.position;
        _maxPointPos = _maxPoint.position;
    }

    private void Update()
    {
        Touch touch;
        Vector2 touchPos;
        if (Input.touchCount == 0)
        {
            _transform.position = _initialPos;
            _isControllerTouched = false;
        }
        else
        {
            touch = Input.GetTouch(0);
            touchPos = touch.position;
            if (CheckIfTouchWithinBorders(touchPos))
            {
                _isControllerTouched = true;
            }
            if (_isControllerTouched)
            {
                Vector2 minStickRange = new Vector2(_initialPos.x - _stickRange, _initialPos.y - _stickRange);
                Vector2 maxStickRange = new Vector2(_initialPos.x + _stickRange, _initialPos.y + _stickRange);

                if (touchPos.x <= minStickRange.x || touchPos.y < minStickRange.y || touchPos.x > maxStickRange.x || touchPos.y > maxStickRange.y)
                {
                    _transform.position = _initialPos + _stickRange * (touchPos - _initialPos).normalized;

                }
                else
                {
                    _transform.position = touchPos;

                }
            }
        }

    }

    public Vector2 GetJoyStickInput()
    {
        return (new Vector2(_transform.position.x, _transform.position.y) - _initialPos).normalized;
    }

    private bool CheckIfTouchWithinBorders(Vector2 touch)
    {
        if (touch.x < _minPointPos.x || touch.y < _minPointPos.y || touch.x > _maxPointPos.x || touch.y > _maxPointPos.y)
            return false;
        return true;
    }

}
