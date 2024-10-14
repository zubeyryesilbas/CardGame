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
    private int cycle;

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
            if (_time > _targetToReach)
            {   
                Debug.Log("Time Out" + cycle);
                cycle += 1;
                _canExecuteTime = false;
                OnTimeOut?.Invoke();
            }
            _time += Time.deltaTime;
        }
    }

    public float GetRemained()
    {
        return _time / _targetToReach;
    }
}
