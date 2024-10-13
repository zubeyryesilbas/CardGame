using System.Collections;
using System.Collections.Generic;
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
}
