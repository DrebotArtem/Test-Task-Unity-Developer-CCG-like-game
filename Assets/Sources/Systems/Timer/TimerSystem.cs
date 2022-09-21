using Entitas;
using UnityEngine;

namespace DrebotGS.Systems
{
  public class TimerSystem : IExecuteSystem, ITearDownSystem
  {
    private readonly TimerContext timer;
    private readonly IGroup<TimerEntity> runningTimers;
    public TimerSystem(Contexts contexts)
    {
      timer = contexts.timer;
      runningTimers = timer.GetGroup(
        TimerMatcher.
        AllOf(TimerMatcher.Timer, TimerMatcher.TimerRunning));
    }

    public void Execute()
    {
      var delta = Time.deltaTime;
      foreach (var e in runningTimers.GetEntities())
      {
        e.timer.remaining -= delta;

        if (e.timer.remaining <= 0.0f)
        {
          e.timer.remaining = 0.0f;
          e.isTimerRunning = false;

          if (e.willDestroyWhenTimerExpires)
          {
            e.isDestroyed = true;
          }
        }
      }
    }

    public void TearDown()
    {
      foreach (var e in runningTimers.GetEntities())
      {
        e.isDestroyed = true;
      }
    }
  }
}
