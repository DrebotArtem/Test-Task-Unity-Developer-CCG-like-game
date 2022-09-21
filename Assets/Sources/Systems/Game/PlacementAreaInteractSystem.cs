using DrebotGS.Config;
using Entitas;
using System.Collections.Generic;
using Zenject;

namespace DrebotGS.Systems
{
  public class PlacementAreaInteractSystem : ReactiveSystem<GameEntity>
  {
    private readonly Contexts _contexts;
    //Injects
    private GameConfig _gameConfig;

    [Inject]
    public void Inject(GameConfig gameConfig)
    {
      _gameConfig = gameConfig;
    }

    public PlacementAreaInteractSystem(Contexts contexts) : base(contexts.game)
    {
      _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.ReadyToDrop.AddedOrRemoved());

    protected override bool Filter(GameEntity entity)
        => !entity.isDestroyed;

    protected override void Execute(List<GameEntity> entities)
    {
      foreach (var entity in entities)
      {
        if(entity.isReadyToDrop)
          _contexts.game.placementAreaEntity.material.value.color = _gameConfig.placementAreaInteractiveColor;
        else
          _contexts.game.placementAreaEntity.material.value.color = _gameConfig.placementAreaBaseColor;
      }
    }
  }
}