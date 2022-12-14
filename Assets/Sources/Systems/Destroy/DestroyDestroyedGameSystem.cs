using Entitas;
using System.Collections.Generic;

namespace DrebotGS.Systems
{
  public class DestroyDestroyedGameSystem : ICleanupSystem
  {
    readonly IGroup<GameEntity> _group;
    readonly List<GameEntity> _buffer = new List<GameEntity>();

    public DestroyDestroyedGameSystem(Contexts contexts)
    {
      _group = contexts.game.GetGroup(GameMatcher.Destroyed);
    }

    public void Cleanup()
    {
      foreach (var e in _group.GetEntities(_buffer))
        e.Destroy();
    }
  }
}