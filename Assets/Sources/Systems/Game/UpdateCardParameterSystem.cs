using Entitas;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DrebotGS.Systems
{
  public class UpdateCardParameterSystem : ReactiveSystem<GameEntity>
  {
    private readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _groupCards;
    private readonly IGroup<GameEntity> _groupPlayerHand;

    public UpdateCardParameterSystem(Contexts contexts) : base(contexts.game)
    {
      _contexts = contexts;
      _groupCards = contexts.game.GetGroup(GameMatcher.
        AllOf(GameMatcher.Card, GameMatcher.Attack, GameMatcher.Health, GameMatcher.Cost).
        NoneOf(GameMatcher.Destroyed, GameMatcher.WaitToDestoy));
      _groupPlayerHand = contexts.game.GetGroup(GameMatcher.PlayerHand);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.UpdateCardParameter);

    protected override bool Filter(GameEntity entity)
        => entity.isUpdateCardParameter && !entity.isDestroyed;

    protected override void Execute(List<GameEntity> entities)
    {
      foreach (var entity in entities)
      {
        entity.isDestroyed = true;
        UpdateRandomParameter();
      }
    }

    private void UpdateRandomParameter()
    {
      int currentCardIndex = _contexts.game.updaterParametersOfCardsEntity.updaterParametersOfCards.currentCardIndex;
      var playerHand = _groupPlayerHand.GetEntities().First(x => x.playerId.value == _contexts.game.updaterParametersOfCardsEntity.playerId.value);
      while (!_groupCards.GetEntities().Any(x => x.cardIndex.value == currentCardIndex))
      {
        if (_groupCards.GetEntities().Length == 0) return;
        currentCardIndex--;
        if (currentCardIndex < 0)
          currentCardIndex = playerHand.currentCardsInHand.value;
      }

      var ent = _groupCards.GetEntities().First(x => x.cardIndex.value == currentCardIndex);
      int randomValue = Random.Range(-2, 10);
      var cardPlayerId = ent.playerId.value;
      switch (Random.Range(0, 3))
      {
        case 0:
          {
            if (ent.hasTimersQueue)
            {
              TimerEntity timer = _contexts.timer.CreateAttackTimer(ent, randomValue, false);
              ent.timersQueue.value.Enqueue(timer);
            }
            else
            {
              TimerEntity timer = _contexts.timer.CreateAttackTimer(ent, randomValue, true);
              Queue<TimerEntity> queueTimers = new Queue<TimerEntity>();
              queueTimers.Enqueue(timer);
              ent.AddTimersQueue(queueTimers);
            }
            Debug.Log("UpdateAttackTo " + randomValue);
            break;
          }
        case 1:
          {
            if (randomValue < 1) ent.isWaitToDestoy = true;
            if (ent.hasTimersQueue)
            {
              TimerEntity timer = _contexts.timer.CreateHealthTimer(ent, randomValue, false);
              ent.timersQueue.value.Enqueue(timer);
            }
            else
            {
              TimerEntity timer = _contexts.timer.CreateHealthTimer(ent, randomValue, true);
              Queue<TimerEntity> queueTimers = new Queue<TimerEntity>();
              queueTimers.Enqueue(timer);
              ent.AddTimersQueue(queueTimers);
            }
            Debug.Log("UpdateHealthTo " + randomValue); 
            break;
          }
        case 2:
          {
            if (ent.hasTimersQueue)
            {
              TimerEntity timer = _contexts.timer.CreateCostTimer(ent, randomValue, false);
              ent.timersQueue.value.Enqueue(timer);
            }
            else
            {
              TimerEntity timer = _contexts.timer.CreateCostTimer(ent, randomValue, true);
              Queue<TimerEntity> queueTimers = new Queue<TimerEntity>();
              queueTimers.Enqueue(timer);
              ent.AddTimersQueue(queueTimers);
            }
            Debug.Log("UpdateCostTo " + randomValue);
            break;
          }
      }

      currentCardIndex--;

      var entityPlayerHand = _groupPlayerHand.GetEntities().FirstOrDefault(x => x.playerId.value == cardPlayerId);
      if(entityPlayerHand == null) return;

      if (currentCardIndex < 0)
        currentCardIndex = entityPlayerHand.currentCardsInHand.value - 1; 

      _contexts.game.updaterParametersOfCardsEntity.ReplaceUpdaterParametersOfCards(currentCardIndex);
    }
  }
}