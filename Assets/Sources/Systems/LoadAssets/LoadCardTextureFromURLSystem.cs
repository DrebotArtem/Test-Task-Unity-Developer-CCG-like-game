using DrebotGS.Config;
using Entitas;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace DrebotGS.Systems
{
  public class LoadCardTextureFromURLSystem : ReactiveSystem<GameEntity>, ITearDownSystem
  {
    private readonly CancellationTokenSource _cts;

    // Injects
    private CardConfig _cardConfig;

    [Inject]
    public void Inject(CardConfig cardConfig)
    {
      _cardConfig = cardConfig;
    }

    public LoadCardTextureFromURLSystem(Contexts contexts) : base(contexts.game)
    {
      _cts = new CancellationTokenSource();
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
      var texture = await LoadTextureFromURL(_cardConfig.CardImageURL);
      if (_cts.Token.IsCancellationRequested) return;

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

    public void TearDown()
    {
      _cts.Cancel();
    }
  }
}