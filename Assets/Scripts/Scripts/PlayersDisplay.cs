using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayersDisplay : MonoBehaviour
{
    [SerializeField] private Slot _playerSlot, _opponentSlot;
    [SerializeField] private BasicHealthBar _playerBar, _opponentBar;
    [SerializeField] private SkillPresenter _skillPresenter;

    public void UpdatePlayerSkill(string header , string description)
    {
        _skillPresenter.UpdateSkill(header ,description);
    }

    public void Show()
    {
        _playerSlot.gameObject.SetActive(true);
        _opponentSlot.gameObject.SetActive(true);
        _playerBar.gameObject.SetActive(true);
        _opponentBar.gameObject.SetActive(true);
        _skillPresenter.gameObject.SetActive(true);
    }
}
