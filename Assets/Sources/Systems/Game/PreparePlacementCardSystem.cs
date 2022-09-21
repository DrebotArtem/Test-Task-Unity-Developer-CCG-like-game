using Entitas;
using UnityEngine;
using Zenject;
using DrebotGS.Config;

namespace DrebotGS.Systems
{
  public class PreparePlacementCardSystem : IExecuteSystem
  {
    private readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _groupSelectedCards;
    private int layer;
    // Injects
    private GameConfig _gameConfig;

    [Inject]
    public void Inject(GameConfig gameConfig)
    {
      _gameConfig = gameConfig;
      layer = (int)Mathf.Log(_gameConfig.placementAreaLayer.value, 2);
    }

    public PreparePlacementCardSystem(Contexts contexts)
    {
      _contexts = contexts;
      _groupSelectedCards = contexts.game.GetGroup(GameMatcher.
        AllOf(GameMatcher.Card, GameMatcher.Selected).
        NoneOf(GameMatcher.Destroyed, GameMatcher.WaitToDestoy));
    }
    public void Execute()
    {
      foreach (var entity in _groupSelectedCards)
        PlacementCard(entity);
    }

    private void PlacementCard(GameEntity entity)
    {
      var distance = 100;
      RaycastHit hit;
      Debug.DrawRay(entity.transform.value.position, 10 * Vector3.down, Color.green);
      if (Physics.Raycast(entity.transform.value.position, Vector3.down, out hit, distance))
      {
        if (hit.transform.gameObject.layer == layer)
        {
          entity.isReadyToDrop = true;
          _contexts.game.placementAreaEntity.isPlacementAreaInteracted = true;
        }
        else
        {
          entity.isReadyToDrop = false;
          _contexts.game.placementAreaEntity.isPlacementAreaInteracted = false;
        }
      }
    }
  }
}

