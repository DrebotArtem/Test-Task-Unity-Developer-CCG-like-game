using DrebotGS.Config;
using Entitas;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DrebotGS.Systems
{
  public class PlacementCardSystem : ReactiveSystem<GameEntity>
  {
    private readonly Contexts _contexts;
    //Injects
    private GameConfig _gameConfig;

    [Inject]
    public void Inject(GameConfig gameConfig)
    {
      _gameConfig = gameConfig;
    }

    public PlacementCardSystem(Contexts contexts) : base(contexts.game)
    {
      _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.OnField);

    protected override bool Filter(GameEntity entity)
        => entity.isCard;

    protected override void Execute(List<GameEntity> entities)
    {
      var entityPlacementArea = _contexts.game.placementAreaEntity;
      foreach (var entity in entities)
        entityPlacementArea.dropCards.entities.Add(entity);

      var startXPosition = entityPlacementArea.transform.value.position.x - ((entityPlacementArea.dropCards.entities.Count - 1f) * _gameConfig.distanceBetweenCards) / 2;
      for (int i = 0; i < entityPlacementArea.dropCards.entities.Count; i++)
      {
        entityPlacementArea.dropCards.entities[i].ReplacePosition(new Vector3(startXPosition + i * _gameConfig.distanceBetweenCards,
          entityPlacementArea.transform.value.position.y + _gameConfig.offsetY,
          entityPlacementArea.transform.value.position.z));
        entityPlacementArea.dropCards.entities[i].ReplaceRotation(Quaternion.identity);
      }
    }
  }
}