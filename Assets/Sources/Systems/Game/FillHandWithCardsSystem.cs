using Entitas;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DrebotGS.Config;

namespace DrebotGS.Systems
{
  public class FillHandWithCardsSystem : ReactiveSystem<GameEntity>
  {
    private readonly Contexts _contexts;
    // Injects
    private GameConfig _gameConfig;

    [Inject]
    public void Inject(GameConfig gameConfig)
    {
      _gameConfig = gameConfig;
    }

    public FillHandWithCardsSystem(Contexts contexts) : base(contexts.game)
    {
      _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.PlayerHand);

    protected override bool Filter(GameEntity entity)
        => entity.isPlayerHand;

    protected override void Execute(List<GameEntity> entities)
    {
      foreach (var entity in entities)
      {
        FillHand(entity);
      }
    }

    private void FillHand(GameEntity entity)
    {
      int startCount = Random.Range(_gameConfig.MinCardsOnStart, _gameConfig.MaxCardsOnStart+1);
      for (int i = 0; i < startCount; i++)
      {
        CreateFirstStackCard(entity, i, entity.playerId.value);
      }
      entity.ReplaceCurrentCardsInHand(startCount);
    }

    private void CreateFirstStackCard(GameEntity entity, int cardIndex, int playerId)
    {
      GameEntity entityCard = _contexts.game.CreateCard("Card");

      entityCard.isInHand = true;
      entityCard.AddPlayerId(playerId);
      entityCard.AddCardIndex(cardIndex);
      entityCard.isFirstStackCard = true;

      entityCard.AddHandTransform(entity.handTransform.value);
      entityCard.AddPlayerHandEntityReference(entity);
    }
  }
}

