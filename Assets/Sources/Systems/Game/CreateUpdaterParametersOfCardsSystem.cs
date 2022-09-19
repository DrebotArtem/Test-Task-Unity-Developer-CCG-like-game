using DrebotGS.Config;
using DrebotGS.Mono;
using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace DrebotGS.Systems
{
  public class CreateUpdaterParametersOfCardsSystem : ReactiveSystem<GameEntity>
  {
   private  readonly Contexts _contexts;

    public CreateUpdaterParametersOfCardsSystem(Contexts contexts) : base(contexts.game)
    {
      _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.PlayerHand);

    protected override bool Filter(GameEntity entity)
        => entity.isPlayerHand;

    protected override void Execute(List<GameEntity> entities)
    {
      GameEntity ent = entities.FirstOrDefault();
      int lastIndex = ent.currentCardsInHand.value - 1;
      var entityUpdater = _contexts.game.CreateEntity();
      entityUpdater.AddUpdaterParametersOfCards(lastIndex);
      entityUpdater.AddPlayerId(ent.playerId.value);
    }
  }
}