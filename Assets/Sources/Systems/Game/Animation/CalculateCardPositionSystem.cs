using DrebotGS.Mono;
using DrebotGS.Services;
using Entitas;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DrebotGS.Systems
{
  public class CalculateCardPositionSystem : ReactiveSystem<GameEntity>
  {
    ILoadService _viewService;
    LocationGame _locationGame;

    [Inject]
    public void Inject(ILoadService viewService, LocationGame locationGame)
    {
      _viewService = viewService;
      _locationGame = locationGame;
    }

    [Inject]
    public void Inject(ILoadService viewService)
    {
      _viewService = viewService;
    }
    public CalculateCardPositionSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.PositionInHand);

    protected override bool Filter(GameEntity entity)
        => entity.hasPositionInHand;

    protected override void Execute(List<GameEntity> entities)
    {
      foreach (var entity in entities)
      {
        InstantiateAsset(entity);
      }
    }

    private void InstantiateAsset(GameEntity entity)
    {
      if (!entity.hasPlayerHandEntityReference)
        return;

      float angle = 0.5f;
      float fDistance = 1;

      var start = -((entity.playerHandEntityReference.value.currentCardsInHand.value - 1f) * angle) / 2; 
      //var start = -((5 - 1f) * angle) / 2;
      var thisPosition = _locationGame.playerHand.position; // entity.handTransform.value.position;
      var thisRotation = _locationGame.playerHand.rotation; //entity.handTransform.value.rotation;
      var val = start + entity.positionInHand.value * angle;
      var rotation = thisRotation * Quaternion.Euler(0, -1 * val, 0);
      var position = thisPosition + rotation * -_locationGame.playerHand.right * fDistance * val; // - entity.handTransform.value.right
      entity.AddPosition(position);
    }
  }
}