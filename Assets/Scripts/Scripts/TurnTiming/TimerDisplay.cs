using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    public void SetFillAmount(float val)
    {
        _fillImage.fillAmount = val;
    }
}
