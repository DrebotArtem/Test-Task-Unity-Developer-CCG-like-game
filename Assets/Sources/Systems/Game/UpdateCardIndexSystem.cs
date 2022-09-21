using Entitas;
using System.Collections.Generic;
using System.Linq;

namespace DrebotGS.Systems
{
  public class UpdateCardIndexSystem : ReactiveSystem<GameEntity>
  {
    private readonly IGroup<GameEntity> _groupCards;
    private readonly List<GameEntity> _bufferCards = new List<GameEntity>();

    public UpdateCardIndexSystem(Contexts contexts) : base(contexts.game)
    {
      _groupCards = contexts.game.GetGroup(GameMatcher.
        AllOf(GameMatcher.Card, GameMatcher.CardIndex, GameMatcher.InHand).
        NoneOf(GameMatcher.Destroyed));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(
          TriggerOnEventMatcherExtension.Added(GameMatcher.Destroyed),
          TriggerOnEventMatcherExtension.Removed(GameMatcher.InHand));

    protected override bool Filter(GameEntity entity)
        => entity.isCard && entity.hasCardIndex;

    protected override void Execute(List<GameEntity> entities)
    {
      foreach (var entity in entities)
        UpdateCardsIndex(entity);
    }
    private void UpdateCardsIndex(GameEntity entity)
    {
      var positionInHand = entity.cardIndex.value;

      var ss = _groupCards.GetEntities(_bufferCards).OrderBy(x => x.cardIndex.value);

      foreach (var item in ss.ToList())
      {
        if (item.cardIndex.value > positionInHand)
          item.ReplaceCardIndex(item.cardIndex.value - 1);
      }
    }
  }
}