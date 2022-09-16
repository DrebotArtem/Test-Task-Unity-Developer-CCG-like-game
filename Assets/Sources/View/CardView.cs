using Entitas;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace DrebotGS.Views
{
  public class CardView : BaseView, ICardTexture2DListener, IGameDestroyedListener
  {
    public Image CardImage;

    public override void Link(IEntity entity)
    {
      base.Link(entity);
      AddListeners();
    }

    public void OnCardTexture2D(GameEntity entity, Texture2D value)
    {
      var sprite = Sprite.Create(value, new Rect(0f, 0f, value.width, value.height), new Vector2(0.5f, 0.5f), 50f, 0, SpriteMeshType.FullRect);
      CardImage.sprite = sprite;
    }

    public void OnDestroyed(GameEntity entity)
    {
      RemoveComponents();
      RemoveListeners();
      Unlink();
      Destroy(gameObject);
    }

    private void AddListeners()
    {
      Entity.AddGameDestroyedListener(this);
      Entity.AddCardTexture2DListener(this);
    }

    private void RemoveListeners()
    {
      Entity.RemoveGameDestroyedListener(this);
      Entity.RemoveCardTexture2DListener(this);
    }

    private void RemoveComponents()
    {       
      Entity.RemoveView();
      Entity.isAsset = false;
    }

    private void OnDestroy()
    {
      if (Entity == null) return;
      RemoveComponents();
      RemoveListeners();
      Unlink();
    }
  }
}