using DrebotGS.Config;
using DrebotGS.Mono;
using Entitas;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace DrebotGS.Systems
{
  public class CalculateCardPositionSystem : ReactiveSystem<GameEntity>
  {
    private readonly Contexts _contexts;

    private readonly IGroup<GameEntity> _groupCards;
    private readonly List<GameEntity> _bufferLoadAsync = new List<GameEntity>();

    //Injects
    private LocationGame _locationGame;
    private CardConfig _cardConfig;

    [Inject]
    public void Inject(CardConfig cardConfig, LocationGame locationGame)
    {
      _cardConfig = cardConfig;
      _locationGame = locationGame;
    }

    public CalculateCardPositionSystem(Contexts contexts) : base(contexts.game)
    {
      _contexts = contexts;
      _groupCards = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Card, GameMatcher.CardIndex).NoneOf(GameMatcher.Destroyed));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(
          TriggerOnEventMatcherExtension.Added(GameMatcher.CardIndex),
          TriggerOnEventMatcherExtension.Added(GameMatcher.Destroyed));

    protected override bool Filter(GameEntity entity)
        => entity.hasCardIndex;

    protected override void Execute(List<GameEntity> entities)
    {
      foreach (var entity in entities)
      {
        if (entity.isDestroyed)
          UpdateCardsIndex(entity);
        else
          ReplacePositionAndRotation(entity);
      }
    }
    private void UpdateCardsIndex(GameEntity entity)
    {
      var positionInHand = entity.cardIndex.value;

      var ss = _groupCards.GetEntities(_bufferLoadAsync).OrderBy(o => o.cardIndex.value);

      foreach (var item in ss.ToList())
      {
        if (item.cardIndex.value > positionInHand)
        {
          item.ReplaceCardIndex(item.cardIndex.value - 1);
        }
      }

      foreach (var ent in _groupCards.GetEntities(_bufferLoadAsync))
      {
          ReplacePositionAndRotation(ent);
      }
    }
    private void ReplacePositionAndRotation(GameEntity entity)
    {
      if (!entity.hasPlayerHandEntityReference)
        return;

      var start = -((entity.playerHandEntityReference.value.currentCardsInHand.value - 1f) * _cardConfig.Angle) / 2;
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