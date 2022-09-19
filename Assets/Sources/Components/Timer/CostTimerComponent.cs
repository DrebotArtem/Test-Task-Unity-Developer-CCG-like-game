using Entitas;

[Timer]
public sealed class CostTimerComponent : IComponent
{
  public int aimTarget;
  public float period;
}