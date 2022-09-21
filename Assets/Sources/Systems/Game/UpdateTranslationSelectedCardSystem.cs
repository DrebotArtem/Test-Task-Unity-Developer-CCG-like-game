using Entitas;
using UnityEngine.InputSystem;
using UnityEngine;

namespace DrebotGS.Systems
{
  public class UpdateTranslationSelectedCardSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _groupSelectedCards;

    public UpdateTranslationSelectedCardSystem(Contexts contexts)
    {
      _groupSelectedCards = contexts.game.GetGroup(GameMatcher.
        AllOf(GameMatcher.Card, GameMatcher.Selected).
        NoneOf(GameMatcher.Destroyed, GameMatcher.WaitToDestoy));
    }
    public void Execute()
    {
      foreach (var entity in _groupSelectedCards)
        UpdateTranslation(entity);
    }

    private void UpdateTranslation(GameEntity entity)
    {
        var screnPositionCard = Camera.main.WorldToScreenPoint(entity.transform.value.position);
        Vector3 position = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, screnPositionCard.z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        entity.ReplaceTranslation(new Vector3(worldPosition.x, entity.transform.value.position.y, worldPosition.z));
    }
  }
}
