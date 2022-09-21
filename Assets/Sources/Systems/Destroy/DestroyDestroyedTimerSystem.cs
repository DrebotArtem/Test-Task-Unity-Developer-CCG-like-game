using Entitas;
using System.Collections.Generic;

namespace DrebotGS.Systems
{
  public sealed class DestroyDestroyedTimerSystem : ICleanupSystem
  {
    readonly IGroup<TimerEntity> _group;
    readonly List<TimerEntity> _buffer = new List<TimerEntity>();

    public DestroyDestroyedTimerSystem(Contexts contexts)
    {
      _group = contexts.timer.GetGroup(TimerMatcher.Destroyed);
    }

    public void Cleanup()
    {
      foreach (var e in _group.GetEntities(_buffer))
        e.Destroy();
    }
  }
}