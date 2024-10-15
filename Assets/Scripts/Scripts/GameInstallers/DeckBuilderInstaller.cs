using System.Collections;
using System.Collections.Generic;
using InputSystem;
using PoolingSystem;
using UnityEngine;
using Zenject;
using SkillSystem;

public class DeckBuilderInstaller : MonoInstaller
{
    [SerializeField] private DeckDisplay _deckDisplay;
    [SerializeField] private GameObject _inputhandlerGameObject;
    [SerializeField] private GameController _gameController;
    [SerializeField] private CardsHolderSo _cardsHolderSo;
    [SerializeField] private CardLayoutCreator _cardLayoutCreator;
    [SerializeField] private PoolController _poolController;
    [SerializeField] private BattleHandler _battleHandler;
    [SerializeField] private SkillsHolderSo _skillsHolderSo;
    [SerializeField] private CardMovementHandler _cardMovementHandler;
    [SerializeField] private EndBattleDisplay _endBattleDisplay;
    [SerializeField] private CardLayoutSettings _cardLayoutSettings;
    private CardFactory _cardFactory;
    public override void InstallBindings()
    {
        _cardFactory = new CardFactory();
        var skillFactory = new SkillFactory(_skillsHolderSo);
        Container.Bind<SkillFactory>().FromInstance(skillFactory).AsSingle();
        Container.Bind<CardsHolderSo>().FromInstance(_cardsHolderSo);
        Container.Bind<DeckDisplay>().FromInstance(_deckDisplay).AsSingle().NonLazy();
        Container.Bind<IInputHandler>().FromInstance(_inputhandlerGameObject.GetComponent<IInputHandler>()).AsSingle().NonLazy();
        Container.Bind<GameController>().FromInstance(_gameController).AsSingle();
        Container.Bind<CardFactory>().FromNew().AsSingle();
        Container.Bind<CardLayoutCreator>().FromInstance(_cardLayoutCreator);
        Container.Bind<PoolController>().FromInstance(_poolController);
        Container.Bind<BattleHandler>().FromInstance(_battleHandler);
        Container.Bind<CardMovementHandler>().FromInstance(_cardMovementHandler).AsSingle();
        Container.Bind<EndBattleDisplay>().FromInstance(_endBattleDisplay).AsSingle();
        Container.Bind<CardLayoutSettings>().FromInstance(_cardLayoutSettings).AsSingle();
    }

    public override void Start()
    {
        Container.Resolve<CardFactory>().Initialize();
    }
}
