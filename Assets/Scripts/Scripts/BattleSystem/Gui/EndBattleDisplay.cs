using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattleDisplay : MonoBehaviour
{
    [SerializeField] private BattleEndAnimation[] _battleEnds;

    private Dictionary<BattleResult, BattleEndAnimation> _animationDic =
        new Dictionary<BattleResult, BattleEndAnimation>();

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        foreach (var item in _battleEnds)
        {
            _animationDic.Add(item.BattleResult , item);
            item.Initialize(() =>
            {
                item.Hide();
            });
        }
    }

    public void ShowBattleEnd(BattleResult battleResult)
    {
        _animationDic[battleResult].Show();
    }
}
