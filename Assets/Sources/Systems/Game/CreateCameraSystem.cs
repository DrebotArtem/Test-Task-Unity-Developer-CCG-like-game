using Entitas;
using UnityEngine;

namespace DrebotGS.Systems
{
  public class CreateCameraSystem : IInitializeSystem
  {
    private readonly Contexts _contexts;

    public CreateCameraSystem(Contexts contexts)
    {
      _contexts = contexts;
    }

    public void Initialize()
    {
      CreateCamera();
    }

    private void CreateCamera()
    {
      GameEntity entity = _contexts.game.CreateEntity();
      entity.AddCamera(Camera.main);
    }
  }
}

