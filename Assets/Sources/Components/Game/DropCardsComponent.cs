using Entitas;
using System.Collections.Generic;

[Game]
public sealed class DropCardsComponent : IComponent
{
  public List<GameEntity> entities;
}
