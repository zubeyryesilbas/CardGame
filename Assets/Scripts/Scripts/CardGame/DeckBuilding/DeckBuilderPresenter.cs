using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DeckBuilderPresenter : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [Inject] private DeckDisplay _deckDisplay;

    private void Start()
    {
       /* _deckDisplay.OnDeckFilled += () =>
        {
            EnableOrDisableStartButton(true);
        };
        _deckDisplay.OnDeckUnFilled += () =>
        {
            EnableOrDisableStartButton(false);
        };*/
    }

    private void EnableOrDisableStartButton(bool val)
    {
        _startButton.interactable = val;
    }
}
