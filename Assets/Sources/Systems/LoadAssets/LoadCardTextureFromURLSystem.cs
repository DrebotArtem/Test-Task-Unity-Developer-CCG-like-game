using DrebotGS.Config;
using Entitas;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace DrebotGS.Systems
{
  public class LoadCardTextureFromURLSystem : ReactiveSystem<GameEntity>
  {
    private GameConfig _gameConfig;

    [Inject]
    public void Inject(GameConfig gameConfig)
    {
      _gameConfig = gameConfig;
    }

    public LoadCardTextureFromURLSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.View);

    protected override bool Filter(GameEntity entity)
        => entity.hasView && entity.isCard;

    protected override void Execute(List<GameEntity> entities)
    {
      foreach (var entity in entities)
      {
        AddCardTexture(entity);
      }
    }

    private async void AddCardTexture(GameEntity entity)
    {
      entity.AddStatusLoadingCardTexture2D(StatusLoading.Loading);
      var texture = await LoadTextureFromURL(_gameConfig.CardImageURL);
      if (texture == null)
      {
        entity.ReplaceStatusLoadingCardTexture2D(StatusLoading.FailLoaded);
        return;
      }

      entity.AddCardTexture2D(texture);
      entity.ReplaceStatusLoadingCardTexture2D(StatusLoading.SuccessLoaded);
    }

    public async Task<Texture2D> LoadTextureFromURL(string url)
    {
      using UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
      var asyncOp = www.SendWebRequest();

      while (asyncOp.isDone == false) 
        await Task.Delay(30);

      if( www.result!=UnityWebRequest.Result.Success )
      {
        Debug.LogWarning($"{www.error}, URL:{www.url}");
        return null;
      }
      else
      {
        return DownloadHandlerTexture.GetContent(www);
      }
    }
  }
}