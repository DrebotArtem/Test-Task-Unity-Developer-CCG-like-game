using Entitas;
using System.Collections.Generic;

[Timer]
public sealed class EntitiesReferenceComponent : IComponent
{
    public List<GameEntity> gameEntities;
}
