using Entitas;

[Timer]
public sealed class HealthTimerComponent : IComponent
{
  public int aimTarget;
  public float period;
}