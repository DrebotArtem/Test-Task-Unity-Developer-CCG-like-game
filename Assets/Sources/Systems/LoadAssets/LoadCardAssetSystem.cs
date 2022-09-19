using DrebotGS.Mono;
using DrebotGS.Services;
using Entitas;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DrebotGS.Systems
{
  public class LoadCardAssetSystem : ReactiveSystem<GameEntity>
  {
    // Injects
    private ILoadService _viewService;
    private LocationGame _locationGame;

    [Inject]
    public void Inject(ILoadService viewService, LocationGame locationGame)
    {
      _viewService = viewService;
      _locationGame = locationGame;
    }

    public LoadCardAssetSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.Asset);

    protected override bool Filter(GameEntity entity)
        => entity.isAsset && entity.isCard && entity.hasAssetName && !entity.hasView;

    protected override void Execute(List<GameEntity> entities)
    {
      foreach (var entity in entities)
      {
        InstantiateAsset(entity);
      }
    }

    private async void InstantiateAsset(GameEntity entity)
    {
      var viewGO = await _viewService.LoadAsset<Transform>(entity, entity.assetName.value);
      if (entity.isEnabled == false || viewGO == null)
        return;

      AddView(entity, viewGO);
      SetParentHand(viewGO);
      SetPoristionAndRotationDeck(viewGO);
    }

    private void AddView(GameEntity entity, Transform viewGO)
    {
      var view = viewGO.GetComponent<IView>();
      view.Link(entity);
      entity.AddView(view);
  }

    private void SetParentHand(Transform viewGO)
    {
      viewGO.transform.SetParent(_locationGame.playerHand);
    }
    private void SetPoristionAndRotationDeck(Transform viewGO)
    {
      viewGO.position = _locationGame.playerDeck.position;
      viewGO.rotation = _locationGame.playerDeck.rotation;
    }
  }
}