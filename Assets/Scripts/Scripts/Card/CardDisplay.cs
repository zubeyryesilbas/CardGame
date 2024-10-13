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
        _nameText.text = name;
        _attackText.text = card.Attack.ToString();
        _deffenceText.text = card.Defense.ToString();
        card.OnDamageTaken += OnDamageTaken;
    }


    public void ApplySkillEffects(SkillEffect[] effects)
    {   
        foreach (var item in effects)
        {   
            switch (item.EffectType)
            {
                case SkillEffectType.IncreaseAttack:
                    IncreaseAttack();
                    break;
                case SkillEffectType.IncreaseDefense:
                    IncreaseDeffense();
                    break;
                case SkillEffectType.DecreaseOpponentAttack:
                    IncreaseAttack();
                    break;
                case SkillEffectType.DecreaseOpponentDefense:
                    IncreaseDeffense();
                    break;
            }
        }
    }

    private void IncreaseAttack()
    {
        _attackText.text = _card.Attack.ToString();
    }

    private void IncreaseDeffense()
    {
        _deffenceText.text = _card.Defense.ToString();
    }
    private void OnDamageTaken(int damage)
    {
        
    }
    
    public void SetHealth(int health)
    {
       
    }

    public void SetAttack(int attack)
    {
       
    }

    
    public void OnGetFromPool()
    {
        
    }

    public void OnReturnToPool()
    {
        
    }
}
