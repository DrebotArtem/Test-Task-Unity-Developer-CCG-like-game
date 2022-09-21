using Entitas;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;
using DrebotGS.Config;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using static UnityEngine.EventSystems.EventTrigger;

namespace DrebotGS.Views
{
  public class CardView : BaseView,
    IAttackListener, IHealthListener, ICostListener,
    ICardIndexListener,
    ICardTexture2DListener,
    IReleaseCardListener,
    IGameDestroyedListener,
    IPositionListener,
    ITranslationListener,
    IOnFieldListener,
    IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
  {
    public AnimationCurve animationCurve;

    public Canvas canvas;
    public Image cardImage;
    public TextMeshProUGUI attackValue;
    public TextMeshProUGUI healthValue;
    public TextMeshProUGUI costValue;

    public GameObject glow;
    public ParticleSystemRenderer glowParticleSystem;

    private Sequence sequence;
    private InputMasterControls _inputControl;
    // Injects
    private CardConfig _cardConfig;

    [Inject]
    public void Inject(CardConfig cardConfig)
    {
      _cardConfig = cardConfig;
    }
    private void InitInputControl()
    {
      _inputControl = new InputMasterControls();
      _inputControl.Enable();
    }
    public override void Link(IEntity entity)
    {
      base.Link(entity);
      AddListeners();
      InitInputControl();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if (Entity.isSelected || Entity.isOnField || Entity.isReleaseCard) return;
      transform.DOKill();
      transform.DOMove(Entity.position.value + _cardConfig.offsetPositionToPointerEnter, _cardConfig.animationDurationToSelect);
      transform.DORotate(_cardConfig.setRotationToPointerEnter, _cardConfig.animationDurationToSelect);
      UpdateSortingOrdersForSelect();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
      if (Entity.isSelected || Entity.isOnField || Entity.isReleaseCard) return;
      ReturnCard();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
      if (Entity.isOnField || Entity.isReleaseCard) return;
      Entity.isSelected = false;
      ReturnCard();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
      if (Entity.isOnField || Entity.isReleaseCard) return;
      Entity.isSelected = true;
      glow.SetActive(true);
    }

    public void OnReleaseCard(GameEntity entity)
    {
      UpdateSortingOrdersByIndex();
      UpdateAttackValue(entity.attack.value);
      UpdateHealthValue(entity.health.value);
      UpdateCostValue(entity.cost.value);

      sequence = DOTween.Sequence();
      sequence.Append(transform.DOMoveX(transform.position.x + _cardConfig.OffsetPositionToShow.x, _cardConfig.AnimationDuration).SetEase(animationCurve));
      sequence.Join(transform.DOMoveY(transform.position.y + _cardConfig.OffsetPositionToShow.y, _cardConfig.AnimationDuration));
      sequence.Join(transform.DOMoveZ(transform.position.z + _cardConfig.OffsetPositionToShow.z, _cardConfig.AnimationDuration));
      sequence.Join(transform.DORotate(transform.rotation.eulerAngles + _cardConfig.OffsetRotationToShow, _cardConfig.AnimationDuration));

      sequence.OnComplete(() =>
      {
        transform.DOMove(entity.position.value, 0.8f);
        transform.DORotateQuaternion(entity.rotation.value, 0.8f).OnComplete(() =>
          { Entity.isReleaseCard = false; });
      });
    }
    public void OnTranslation(GameEntity entity, Vector3 value)
    {
      transform.position = value;
    }
    public void OnOnField(GameEntity entity)
    {
      transform.DOKill();
      transform.DOMove(Entity.position.value, _cardConfig.animationDurationToUnselect);
      transform.DORotate(Entity.rotation.value.eulerAngles, _cardConfig.animationDurationToUnselect);
    }
    public void OnCardIndex(GameEntity entity, int value)
    {
      UpdateSortingOrdersByIndex();
    }
    public void OnAttack(GameEntity entity, int value)
    {
      UpdateAttackValue(value);
    }
    public void OnHealth(GameEntity entity, int value)
    {
      UpdateHealthValue(value);
    }
    public void OnCost(GameEntity entity, int value)
    {
      UpdateCostValue(value);
    }
    public void OnCardTexture2D(GameEntity entity, Texture2D value)
    {
      var sprite = Sprite.Create(value, new Rect(0f, 0f, value.width, value.height), new Vector2(0.5f, 0.5f), 50f, 0, SpriteMeshType.FullRect);
      cardImage.sprite = sprite;
    }
    public void OnPosition(GameEntity entity, Vector3 value)
    {
      UpdatePositionInHand();
    }
    public void OnDestroyed(GameEntity entity)
    {
      RemoveComponents();
      RemoveListeners();
      Unlink();
      Destroy(gameObject);
    }

    private void UpdateAttackValue(int value)
    {
      attackValue.text = value.ToString();
    }
    private void UpdateHealthValue(int value)
    {
      healthValue.text = value.ToString();
    }
    private void UpdateCostValue(int value)
    {
      costValue.text = value.ToString();
    }
    private void UpdateSortingOrdersByIndex()
    {
      var newsortingOrder = _cardConfig.startSortingOrder - Entity.cardIndex.value;
      canvas.sortingOrder = newsortingOrder;
      glowParticleSystem.sortingOrder = newsortingOrder;
    }
    private void UpdateSortingOrdersForSelect()
    {
      var newsortingOrder = _cardConfig.startSortingOrder + 1;
      canvas.sortingOrder = newsortingOrder;
      glowParticleSystem.sortingOrder = newsortingOrder;
    }
    private void UpdatePositionInHand()
    {
      transform.DOKill();

      transform.DOMove(Entity.position.value, 0.8f);
      transform.DORotateQuaternion(Entity.rotation.value, 0.8f);
    }
    private void ReturnCard()
    {
      transform.DOKill();
      transform.DOMove(Entity.position.value, _cardConfig.animationDurationToUnselect);
      transform.DORotate(Entity.rotation.value.eulerAngles, _cardConfig.animationDurationToUnselect);
      UpdateSortingOrdersByIndex();
      glow.SetActive(false);
    }

    private void OnDisable()
    {
      if(sequence != null)
        sequence.Kill();
      transform.DOKill();
    }

    private void AddListeners()
    {
      Entity.AddAttackListener(this);
      Entity.AddHealthListener(this);
      Entity.AddCostListener(this);
      Entity.AddGameDestroyedListener(this);
      Entity.AddCardTexture2DListener(this);
      Entity.AddReleaseCardListener(this);
      Entity.AddPositionListener(this);
      Entity.AddCardIndexListener(this);
      Entity.AddTranslationListener(this);
    }
    private void RemoveListeners()
    {
      Entity.RemoveAttackListener(this);
      Entity.RemoveHealthListener(this);
      Entity.RemoveCostListener(this);
      Entity.RemoveGameDestroyedListener(this);
      Entity.RemoveCardTexture2DListener(this);
      Entity.RemoveReleaseCardListener(this);
      Entity.RemovePositionListener(this);
      Entity.RemoveCardIndexListener(this);
      Entity.RemoveTranslationListener(this);
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