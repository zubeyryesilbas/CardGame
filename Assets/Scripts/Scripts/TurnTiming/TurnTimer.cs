using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTimer
{
    private float _targetToReach;
    private float _time;
    private bool _canExecuteTime;
    public Action OnTimeOut;

    public TurnTimer(float targetToReach)
    {
        _targetToReach = targetToReach;
    }

    public void EnableProcess(bool val)
    {
        _canExecuteTime = val;
    }

    public void ResetTimer()
    {
        _time = 0f;
    }
    public void ExecuteTime()
    {
        if (_canExecuteTime)
        {
            _time += Time.deltaTime;
            if (_time >= _targetToReach)
            {
                _canExecuteTime = false;
                OnTimeOut?.Invoke();
            }
        }
    }

    public float GetRemained()
    {
        return _time / _targetToReach;
    }
}
