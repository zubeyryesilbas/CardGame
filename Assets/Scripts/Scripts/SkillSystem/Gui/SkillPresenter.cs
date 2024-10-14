using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class SkillPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _skillNameText, _skillDescriptionText;

    public void UpdateSkill(string name , string description)
    {
        _skillNameText.text = name;
        _skillDescriptionText.text = description;
    }

    public void HideSkill()
    {
        _skillNameText.DOFade(0, 0.5f);
        _skillDescriptionText.DOFade(0, 0.5f);
    }
}
