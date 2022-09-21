using Entitas;
using ModestTree;
using System.Collections.Generic;

namespace DrebotGS.Systems
{
  public class CostTimerSystem : ReactiveSystem<TimerEntity>
  {
    private readonly TimerContext _context;

    public CostTimerSystem(Contexts contexts) : base(contexts.timer)
    {
      _context = contexts.timer;
    }

    protected override ICollector<TimerEntity> GetTrigger(IContext<TimerEntity> context)
        => context.CreateCollector(TimerMatcher.TimerRunning.Removed());

    protected override bool Filter(TimerEntity entity)
        => entity.hasCostTimer && entity.hasTimer && !entity.isTimerRunning;

    protected override void Execute(List<TimerEntity> entities)
    {
      foreach (var entity in entities)
      {
        if (entity.hasEntityReference)
        {
          GameEntity entityModify = entity.entityReference.gameEntity;
          if (entityModify == null || !entityModify.isEnabled || !entityModify.hasCost)
            continue;

          int targetValue = entity.costTimer.aimTarget;

          if (targetValue > entityModify.cost.value)
            entityModify.ReplaceCost(entityModify.cost.value + 1);
          else if (targetValue < entityModify.cost.value)
            entityModify.ReplaceCost(entityModify.cost.value - 1);

          if (targetValue == entityModify.cost.value)
          {
            entityModify.timersQueue.value.Dequeue();
            if (entityModify.timersQueue.value.IsEmpty()) { entityModify.RemoveTimersQueue(); continue; }
            entityModify.timersQueue.value.Peek().isTimerRunning = true;
            continue;
          }

          _context.CreateCostTimer(entityModify, targetValue);
        }
      }

    }
  }
}