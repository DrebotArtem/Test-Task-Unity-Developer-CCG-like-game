using Entitas;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;
using DrebotGS.Config;

namespace DrebotGS.Views
{
  public class CardView : BaseView, ICardTexture2DListener, IReleaseCardListener, IGameDestroyedListener
  {
    Sequence sequence;
    public Image cardImage;
    public AnimationCurve animationCurve;

    private CardConfig _cardConfig;

    [Inject]
    public void Inject(CardConfig cardConfig)
    {
      _cardConfig = cardConfig;
    }

    public override void Link(IEntity entity)
    {
      base.Link(entity);
      AddListeners();
    }

    public void OnCardTexture2D(GameEntity entity, Texture2D value)
    {
      var sprite = Sprite.Create(value, new Rect(0f, 0f, value.width, value.height), new Vector2(0.5f, 0.5f), 50f, 0, SpriteMeshType.FullRect);
      cardImage.sprite = sprite;
    }

    public void OnReleaseCard(GameEntity entity)
    {
      sequence = DOTween.Sequence();
      sequence.Append(transform.DOMoveX(transform.position.x + _cardConfig.OffsetPositionToShow.x, _cardConfig.AnimationDuration).SetEase(animationCurve));
      sequence.Join(transform.DOMoveY(transform.position.y + _cardConfig.OffsetPositionToShow.y, _cardConfig.AnimationDuration));
      sequence.Join(transform.DOMoveZ(transform.position.z + _cardConfig.OffsetPositionToShow.z, _cardConfig.AnimationDuration));

      sequence.Join(transform.DORotate(transform.rotation.eulerAngles + _cardConfig.OffsetRotationToShow, _cardConfig.AnimationDuration));


      sequence.OnComplete(() =>
      {
        transform.DOMove(entity.position.value, 0.8f);
        transform.DOLocalRotate(Vector3.zero, 0.8f);
      });
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
      Entity.AddReleaseCardListener(this);
    }

    private void RemoveListeners()
    {
      Entity.RemoveGameDestroyedListener(this);
      Entity.RemoveCardTexture2DListener(this);
      Entity.RemoveReleaseCardListener(this);
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