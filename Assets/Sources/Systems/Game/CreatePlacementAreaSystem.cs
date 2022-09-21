using DrebotGS.Mono;
using Entitas;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DrebotGS.Systems
{
  public class CreatePlacementAreaSystem : IInitializeSystem
  {
    private readonly Contexts _contexts;
    // Injects
    private LocationGame _locationGame;

    [Inject]
    public void Inject(LocationGame locationGame)
    {
      _locationGame = locationGame;
    }

    public CreatePlacementAreaSystem(Contexts contexts) 
    {
      _contexts = contexts;
    }
    public void Initialize()
    {
      var entityPlacementArea = _contexts.game.CreateEntity();
      entityPlacementArea.isPlacementArea = true;
      entityPlacementArea.AddTransform(_locationGame.playerPlacementArea.transform);
      entityPlacementArea.AddMaterial(_locationGame.playerPlacementArea.transform.GetComponent<Renderer>().material);
      entityPlacementArea.AddDropCards(new List<GameEntity>());
    }
  }
}