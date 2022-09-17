using DrebotGS.Mono;
using Entitas;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using Zenject;
using static UnityEngine.EventSystems.EventTrigger;

namespace DrebotGS.Systems
{
  public class FillHandWithCardsSystem : ReactiveSystem<GameEntity>
  {
    private readonly Contexts _contexts;

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
      int startCount = Random.Range(4, 6);
      for (int i = 0; i < startCount; i++)
      {
        CreateFirstStackCard(entity, i);
        entity.ReplaceCurrentCardsInHand(i);
      }
    }

    private void CreateFirstStackCard(GameEntity entity, int positionInHand)
    {
      GameEntity entityCard = _contexts.game.CreateEntity();
      entityCard.isCard = true;
      entityCard.AddNameID("Card");
      entityCard.AddPositionInHand(positionInHand);
      entityCard.isAsset = true;

      entityCard.isFirstStackCard = true;

      entityCard.AddHandTransform(entity.handTransform.value);
      entityCard.AddPlayerHandEntityReference(entity);
    }
  }
}

