using Entitas;
using System.Collections.Generic;

namespace DrebotGS.Systems
{
  public class ReleaseFirstStackCardsSystem : ReactiveSystem<GameEntity>
  {
    private readonly IGroup<GameEntity> _groupView;
    private readonly IGroup<GameEntity> _groupFirstStackCards;

    public ReleaseFirstStackCardsSystem(Contexts contexts) : base(contexts.game)
    {
      _groupView = contexts.game.GetGroup(GameMatcher.View);
      _groupFirstStackCards = contexts.game.GetGroup(GameMatcher.FirstStackCard);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.View);

    protected override bool Filter(GameEntity entity)
        => entity.hasView && entity.isFirstStackCard;

    protected override void Execute(List<GameEntity> entities)
    {
      if (_groupFirstStackCards.count == _groupView.count)
      {
        foreach (var entity in _groupFirstStackCards.GetEntities())
        {
          entity.isReleaseCard = true;
        }
      }
    }
  }
}

