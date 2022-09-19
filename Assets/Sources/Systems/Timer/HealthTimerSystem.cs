using Entitas;
using ModestTree;
using System.Collections.Generic;

namespace DrebotGS.Systems
{
  public class HealthTimerSystem : ReactiveSystem<TimerEntity>
  {
    private readonly Contexts _contexts;

    public HealthTimerSystem(Contexts contexts) : base(contexts.timer)
    {
      _contexts = contexts;
    }

    protected override ICollector<TimerEntity> GetTrigger(IContext<TimerEntity> context)
        => context.CreateCollector(TimerMatcher.TimerRunning.Removed());

    protected override bool Filter(TimerEntity entity)
        => entity.hasHealthTimer && entity.hasTimer && !entity.isTimerRunning;

    protected override void Execute(List<TimerEntity> entities)
    {
      foreach (var entity in entities)
      {
        if (entity.hasEntityReference)
        {
          GameEntity entityModify = entity.entityReference.gameEntity;
          if (entityModify == null || !entityModify.isEnabled || !entityModify.hasHealth)
            continue;

          int targetValue = entity.healthTimer.aimTarget;

          if (targetValue > entityModify.health.value)
            entityModify.ReplaceHealth(entityModify.health.value + 1);
          else if (targetValue < entityModify.health.value)
            entityModify.ReplaceHealth(entityModify.health.value - 1);

          if (targetValue == entityModify.health.value)
          {
           entityModify.timersQueue.value.Dequeue();
            if (entityModify.timersQueue.value.IsEmpty()) { entityModify.RemoveTimersQueue(); continue; }
            entityModify.timersQueue.value.Peek().isTimerRunning = true;
            continue;
          }

          _contexts.timer.CreateHealthTimer(entityModify, targetValue);
        }
      }

    }
  }
}