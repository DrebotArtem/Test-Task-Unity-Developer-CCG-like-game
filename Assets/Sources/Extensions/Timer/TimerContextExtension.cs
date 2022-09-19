public static class TimerContextExtension
{
  public static TimerEntity CreateAttackTimer(this TimerContext context, GameEntity entity, int aimValue, bool isTimerRunning = true, float time = 0.25f)
  {
    var timer = context.CreateEntity();
    timer.AddAttackTimer(aimValue, time);
    timer.AddTimer(timer.attackTimer.period);
    timer.willDestroyWhenTimerExpires = true;
    timer.isTimerRunning = isTimerRunning;
    timer.AddEntityReference(entity);
    return timer;
  }
  public static TimerEntity CreateHealthTimer(this TimerContext context, GameEntity entity, int aimValue, bool isTimerRunning = true, float time = 0.25f)
  {
    var timer = context.CreateEntity();
    timer.AddHealthTimer(aimValue, time);
    timer.AddTimer(timer.healthTimer.period);
    timer.willDestroyWhenTimerExpires = true;
    timer.isTimerRunning = isTimerRunning;
    timer.AddEntityReference(entity);
    return timer;
  }

  public static TimerEntity CreateCostTimer(this TimerContext context, GameEntity entity, int aimValue, bool isTimerRunning = true, float time = 0.25f)
  {
    var timer = context.CreateEntity();
    timer.AddCostTimer(aimValue, time);
    timer.AddTimer(timer.costTimer.period);
    timer.willDestroyWhenTimerExpires = true;
    timer.isTimerRunning = isTimerRunning;
    timer.AddEntityReference(entity);
    return timer;
  }
}
