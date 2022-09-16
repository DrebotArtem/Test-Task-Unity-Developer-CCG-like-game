using Entitas;

namespace DrebotGS.Systems
{
  public class CreateCardSystem : IInitializeSystem
  {
    private readonly Contexts _contexts;

    public CreateCardSystem(Contexts contexts)
    {
      _contexts = contexts;
    }

    public void Initialize()
    {
      CreateTestObject();
    }

    private void CreateTestObject()
    {
      GameEntity entity = _contexts.game.CreateEntity();
      entity.isCard = true;
      entity.AddNameID("Card");
      entity.isAsset = true;
      GameEntity entity2 = _contexts.game.CreateEntity();
      entity2.isCard = true;
      entity2.AddNameID("Card");
      entity2.isAsset = true;
    }
  }
}

