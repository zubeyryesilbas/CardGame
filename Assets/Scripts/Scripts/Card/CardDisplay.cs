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
    public Transform CardTr => transform;
    public string CardName => _cardName;
    private string _cardName;
    private Card _card;
    public Card Card => _card;
    public PoolType PoolType { get; }
    public GameObject PoolObj => gameObject;
    public Action<int> DamageCallBack { get; set; }
    
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
        switch (effect.EffectType)
        {
            case SkillEffectType.IncreaseAttack:
            case SkillEffectType.DecreaseOpponentAttack:
                UpdateAttack(effect.EffectValue);
                break;
            case SkillEffectType.IncreaseDefense:
            case SkillEffectType.DecreaseOpponentDefense:
                UpdateDeffense(effect.EffectValue);
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
        
    }
    
    public void OnGetFromPool()
    {
        
    }

    public void OnReturnToPool()
    {
        
    }
}
