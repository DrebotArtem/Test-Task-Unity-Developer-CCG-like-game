using Entitas;
using System.Collections.Generic;

[Game]
public sealed class TimersQueueComponent : IComponent
{
  public Queue<TimerEntity> value;
}