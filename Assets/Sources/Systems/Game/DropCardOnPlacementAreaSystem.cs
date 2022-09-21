using Entitas;
using System.Collections.Generic;

namespace DrebotGS.Systems
{
  public class DropCardOnPlacementAreaSystem : ReactiveSystem<GameEntity>
  {
    public DropCardOnPlacementAreaSystem(Contexts contexts) : base(contexts.game) { }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.Selected.Removed());

    protected override bool Filter(GameEntity entity)
        => entity.isCard && entity.isReadyToDrop;

    protected override void Execute(List<GameEntity> entities)
    {
      foreach (var entity in entities)
        DropCard(entity);
    }

    private void DropCard(GameEntity entity)
    {
      entity.isReadyToDrop = false;
      entity.isInHand = false;
      entity.isOnField = true;
    }
  }
}

