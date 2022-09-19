using DrebotGS.Config;
using DrebotGS.Mono;
using Entitas;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DrebotGS.Systems
{
  public class UpdateCurrentCardsSystem : ReactiveSystem<GameEntity>
  {
    //Injects
    private LocationGame _locationGame;
    private CardConfig _cardConfig;

    [Inject]
    public void Inject(CardConfig cardConfig, LocationGame locationGame)
    {
      _cardConfig = cardConfig;
      _locationGame = locationGame;
    }
    readonly Contexts _contexts;

    readonly IGroup<GameEntity> _groupLoadAsync;
    readonly List<GameEntity> _bufferLoadAsync = new List<GameEntity>();

    public UpdateCurrentCardsSystem(Contexts contexts) : base(contexts.game)
    {
      _contexts = contexts;
      _groupLoadAsync = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.PlayerHand, GameMatcher.CurrentCardsInHand));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.Destroyed);

    protected override bool Filter(GameEntity entity)
        => entity.isCard;

    protected override void Execute(List<GameEntity> entities)
    {
      foreach (var entity in _groupLoadAsync.GetEntities(_bufferLoadAsync))
      {
        var newCountCards = entity.currentCardsInHand.value - 1;
        entity.ReplaceCurrentCardsInHand(newCountCards);
      }
    }

    private void InstantiateAsset(GameEntity entity)
    {
      if (!entity.hasPlayerHandEntityReference)
        return;

      var start = -((entity.playerHandEntityReference.value.currentCardsInHand.value - 1f) * _cardConfig.Angle) / 2;
      var thisPosition = _locationGame.playerHand.position; // entity.handTransform.value.position;
      var thisRotation = _locationGame.playerHand.rotation; //entity.handTransform.value.rotation;
      var val = start + entity.cardIndex.value * _cardConfig.Angle;
      var rotation = thisRotation * Quaternion.Euler(0, -1 * val, 0);
      var position = thisPosition + rotation * -_locationGame.playerHand.right * _cardConfig.Distance * val; // - entity.handTransform.value.right
      entity.AddPosition(position);
      entity.AddRotation(rotation);
    }
  }
}