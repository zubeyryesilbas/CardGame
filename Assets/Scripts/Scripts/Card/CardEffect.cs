using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PoolingSystem;
using TMPro;
using UnityEngine;
// Temporary Solution
public class CardEffect : MonoBehaviour
{   
    private float animationDuration = 0.5f;
    [SerializeField] private TextMeshPro _effectText;

    public void PlayEffect(SkillEffect effect)
    {   
        _effectText.gameObject.SetActive(true);
        _effectText.transform.localPosition = Vector3.zero;
        switch (effect.EffectType)
        {   
            case SkillEffectType.OpponentAttackBoostNextTurn:
            case SkillEffectType.IncreaseAttack:
                _effectText.text = "Attack Boost " + effect.EffectValue;
                _effectText.color = Color.green;
                break;
            case SkillEffectType.IncreaseDefense:
                _effectText.text =  "Deffense Boost " + effect.EffectValue;
                _effectText.color = Color.green;
                break;
            case SkillEffectType.DecreaseOpponentAttack:
                _effectText.text = "Attack Reduction " + - effect.EffectValue;
                _effectText.color = Color.red;
                break;
            case SkillEffectType.DecreaseOpponentDefense:
                _effectText.text = "Deffense Reduction " + -effect.EffectValue;
                _effectText.color = Color.red;
                break;
        }

        _effectText.transform.DOLocalMoveY(_effectText.transform.localPosition.y + 5f, 0.5f).OnComplete(() =>
        {
            _effectText.DOFade(0, 0.5f).OnComplete(() =>
            {
                _effectText.gameObject.SetActive(false);
            });
        });
    }
    
}

