using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShieldEffect : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private GameObject _graphics;

    public void ActivateEffect(int amount)
    {
        _graphics.SetActive(true);
        _text.text = amount.ToString();
    }
}
