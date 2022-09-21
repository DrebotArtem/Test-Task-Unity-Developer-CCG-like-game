using DrebotGS.Config;
using DrebotGS.Mono;
using Entitas;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace DrebotGS.Systems
{
  public class CalculateCardPositionInHandSystem : ReactiveSystem<GameEntity>
  {
    private readonly IGroup<GameEntity> _groupPlayersHands;
    private readonly IGroup<GameEntity> _groupCards;
    //Injects
    private LocationGame _locationGame;
    private CardConfig _cardConfig;

    [Inject]
    public void Inject(CardConfig cardConfig, LocationGame locationGame)
    {
      _cardConfig = cardConfig;
      _locationGame = locationGame;
    }

    public CalculateCardPositionInHandSystem(Contexts contexts) : base(contexts.game)
    {
      _groupCards = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Card, GameMatcher.CardIndex, GameMatcher.InHand).NoneOf(GameMatcher.Destroyed));
      _groupPlayersHands = contexts.game.GetGroup(GameMatcher.PlayerHand);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(
          TriggerOnEventMatcherExtension.Added(GameMatcher.CardIndex),
          TriggerOnEventMatcherExtension.Added(GameMatcher.Destroyed),
          TriggerOnEventMatcherExtension.Removed(GameMatcher.InHand));

    protected override bool Filter(GameEntity entity)
        => entity.hasCardIndex;

    protected override void Execute(List<GameEntity> entities)
    {
      foreach (var entity in entities)
      {
        var hand = _groupPlayersHands.GetEntities().FirstOrDefault(x => x.playerId.value == entity.playerId.value);
        foreach (var ent in _groupCards)
          ReplacePositionAndRotation(hand.currentCardsInHand.value, ent);
      }
    }

    private void ReplacePositionAndRotation(int curentCardsInHand, GameEntity entity)
    {
      var start = -((curentCardsInHand - 1f) * _cardConfig.Angle) / 2;
      var thisPosition = _locationGame.playerHand.position;
      var thisRotation = _locationGame.playerHand.rotation;
      var val = start + entity.cardIndex.value * _cardConfig.Angle;
      var rotation = thisRotation * Quaternion.Euler(0, -1 * val, 0);
      var position = thisPosition + rotation * -_locationGame.playerHand.right * _cardConfig.Distance * val;
      position += _locationGame.playerHand.up * _cardConfig.FrontOffset * entity.cardIndex.value;
      entity.ReplacePosition(position);
      entity.ReplaceRotation(rotation);
    }
  }
}