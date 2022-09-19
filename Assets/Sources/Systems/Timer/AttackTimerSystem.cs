using Entitas;
using ModestTree;
using System.Collections.Generic;

namespace DrebotGS.Systems
{
  public class AttackTimerSystem : ReactiveSystem<TimerEntity>
  {
    private readonly Contexts _contexts;

    public AttackTimerSystem(Contexts contexts) : base(contexts.timer)
    {
      _contexts = contexts;
    }

    protected override ICollector<TimerEntity> GetTrigger(IContext<TimerEntity> context)
        => context.CreateCollector(TimerMatcher.TimerRunning.Removed());

    protected override bool Filter(TimerEntity entity)
        => entity.hasAttackTimer && entity.hasTimer && !entity.isTimerRunning;

    protected override void Execute(List<TimerEntity> entities)
    {
      foreach (var entity in entities)
      {
        if (entity.hasEntityReference)
        {
          GameEntity entityModify = entity.entityReference.gameEntity;
          if (entityModify == null || !entityModify.isEnabled || !entityModify.hasAttack)
            continue;

          int targetValue = entity.attackTimer.aimTarget;

          if (targetValue > entityModify.attack.value)
            entityModify.ReplaceAttack(entityModify.attack.value + 1);
          else if (targetValue < entityModify.attack.value)
            entityModify.ReplaceAttack(entityModify.attack.value - 1);

          if (targetValue == entityModify.attack.value)
          {
            entityModify.timersQueue.value.Dequeue();
            if (entityModify.timersQueue.value.IsEmpty()) { entityModify.RemoveTimersQueue(); continue; }
            entityModify.timersQueue.value.Peek().isTimerRunning = true;
            continue;
          }

          _contexts.timer.CreateAttackTimer(entityModify, targetValue);
        }
      }

    }
  }
}