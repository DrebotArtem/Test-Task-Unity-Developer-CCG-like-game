using Entitas;

[Timer]
public sealed class AttackTimerComponent : IComponent
{
  public int aimTarget;
  public float period;
}