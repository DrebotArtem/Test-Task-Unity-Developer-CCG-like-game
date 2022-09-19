using Entitas;
using System.Collections.Generic;

namespace DrebotGS.Systems
{
  public class DestroyCardWithLowHealthSystem : ReactiveSystem<GameEntity>
  {
    public DestroyCardWithLowHealthSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.Health);

    protected override bool Filter(GameEntity entity)
        => entity.isCard && entity.hasHealth;

    protected override void Execute(List<GameEntity> entities)
    {
      foreach (var entity in entities)
      {
        DestroyCardWithoutHealth(entity);
      }
    }

    private void DestroyCardWithoutHealth(GameEntity entity)
    {
      if (entity.health.value < 1)
        entity.isDestroyed = true;
    }
  }
}

