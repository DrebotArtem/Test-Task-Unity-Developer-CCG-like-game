using Entitas;
using System.Collections.Generic;
using System.Linq;

namespace DrebotGS.Systems
{
  public class UpdateCurrentCardsSystem : ReactiveSystem<GameEntity>
  {
    readonly IGroup<GameEntity> _groupPlayersHands;
    readonly IGroup<GameEntity> _groupCardsInHand;

    public UpdateCurrentCardsSystem(Contexts contexts) : base(contexts.game)
    {
      _groupPlayersHands = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.PlayerHand, GameMatcher.CurrentCardsInHand));
      _groupCardsInHand = contexts.game.GetGroup(GameMatcher.
        AllOf(GameMatcher.Card, GameMatcher.InHand).
        NoneOf(GameMatcher.Destroyed));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(
          GameMatcher.Destroyed.Added(),
         GameMatcher.InHand.Removed());

    protected override bool Filter(GameEntity entity)
        => entity.isCard;

    protected override void Execute(List<GameEntity> entities)
    {
      foreach (var entity in entities)
      {
        var playerHand = _groupPlayersHands.GetEntities().FirstOrDefault(x => x.playerId.value == entity.playerId.value);
        var cardsInHand = _groupCardsInHand.GetEntities().Where(x => x.playerId.value == entity.playerId.value);
        playerHand.ReplaceCurrentCardsInHand(cardsInHand.Count());
      }
    }
  }
}