using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PoolingSystem;
using TMPro;
using UnityEngine;

public class CardDisplay : MonoBehaviour, ICard,IPooledObject
{
    [SerializeField] private SpriteRenderer _cardIconRenderer, _frameRenderer, _labelBgRenderer;
    [SerializeField] private TextMeshPro _nameText, _attackText, _deffenceText;
    [SerializeField] private int _orderInLayer = 1;
    [SerializeField] private TextAnimation _attackTextAnim, _deffenceTextAnim;
    [SerializeField] private CardEffect _effect;
    public Transform CardTr => transform;
    private Card _card;
    public Card Card => _card;
    public PoolType PoolType { get; }
    public GameObject PoolObj => gameObject;
    
    public void Initialize(Sprite icon, Card card)
    {
        _frameRenderer.sortingOrder = _orderInLayer;
        _cardIconRenderer.sortingOrder = _orderInLayer;
        _nameText.sortingOrder = _orderInLayer;
        _attackText.sortingOrder = _orderInLayer;
        _deffenceText.sortingOrder = _orderInLayer;
        _labelBgRenderer.sortingOrder = _orderInLayer;
        _card = card;
        _cardIconRenderer.sprite = icon;
        _nameText.text =_card.Name;
        _attackText.text = card.Attack.ToString();
        _deffenceText.text = card.Defense.ToString();
        _attackTextAnim.SetInitialValue(card.Attack);
        _deffenceTextAnim.SetInitialValue(card.Defense);
        card.OnDamageTaken += OnDamageTaken;
    }


    public void ApplySkillEffect(SkillEffect effect)
    {       
        _effect.PlayEffect(effect);
        switch (effect.EffectType)
        {   
            case SkillEffectType.OpponentAttackBoostNextTurn:
            case SkillEffectType.IncreaseAttack:
                UpdateAttack(effect.EffectValue);
                break;
            case SkillEffectType.DecreaseOpponentAttack:
                UpdateAttack(-effect.EffectValue);
                break;
            case SkillEffectType.IncreaseDefense:
                UpdateDeffense(effect.EffectValue);
                break;
            case SkillEffectType.DecreaseOpponentDefense:
                UpdateDeffense(-effect.EffectValue);
                break;
        }
    }

    private void UpdateAttack(int val)
    {   
        _attackTextAnim.AnimateTextValue(Card.Attack);
    }

    private void UpdateDeffense(int val)
    {  
        _deffenceTextAnim.AnimateTextValue(Card.Defense);
    }
    private void OnDamageTaken(int damage)
    {
        transform.DOShakePosition(1f, new Vector3(1, 1, 0) * 0.05f, 50,0, false, true);
        _deffenceTextAnim.AnimateTextValue(_card.Defense);
    }
    
    public void OnGetFromPool()
    {
        
    }

    public void OnReturnToPool()
    {
        
    }
}
