using Lean.Touch;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static event Action<float> MoveLeft = delegate { };
    public static event Action<float> MoveRight = delegate { };
    public static event Action Shoot = delegate { };
    public static event Action Jump = delegate { };

    public LeanScreenDepth ScreenDepth;

    private bool _prevFrameTouchEnabled = false;
    private bool _currentFrameTouchEnabled = false;

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    protected virtual void LateUpdate()
    {
        var fingers = LeanTouch.GetFingers(false, false, 1);
        if (fingers.Count >= 1)
        {
            _currentFrameTouchEnabled = true;
            var lastScreenPoint = LeanGesture.GetLastScreenCenter(fingers);
            var screenPoint = LeanGesture.GetScreenCenter(fingers);

            var worldDelta = lastScreenPoint - screenPoint;

            if (worldDelta.x > 0)
            {
                MoveLeft(Mathf.Abs(worldDelta.x));
            }
            else if (worldDelta.x < 0)
            {
                MoveRight(Mathf.Abs(worldDelta.x));
            }
            Shoot();
        }
        else _currentFrameTouchEnabled = false;

        if (_prevFrameTouchEnabled && !_currentFrameTouchEnabled) Jump();

        _prevFrameTouchEnabled = _currentFrameTouchEnabled;
    }
}
