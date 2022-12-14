using DrebotGS.Mono;
using Entitas;
using Zenject;

namespace DrebotGS.Systems
{
  public class CreateHandSystem : IInitializeSystem
  {
    private readonly Contexts _contexts;
    // Injects
    private LocationGame _locationGame;

    [Inject]
    public void Inject(LocationGame locationGame)
    {
      _locationGame = locationGame;
    }

    public CreateHandSystem(Contexts contexts)
    {
      _contexts = contexts;
    }

    public void Initialize()
    {
      CreateHand();
    }

    private void CreateHand()
    {
      GameEntity entity = _contexts.game.CreateEntity();
      entity.isPlayerHand = true;
      entity.AddPlayerId(0);
      entity.AddMaxCardsInHand(10);
      entity.AddCurrentCardsInHand(0);
      entity.AddHandTransform(_locationGame.playerHand);
    }
  }
}

