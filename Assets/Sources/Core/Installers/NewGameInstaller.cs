using DrebotGS.Mono;
using DrebotGS.Systems;
using Zenject;

namespace DrebotGS.Core
{
  public class NewGameInstaller : MonoInstaller
  {
    public LocationGame LocationGame;

    private InjectableFeature _gameSystems;
    //Injects
    private DiContainer _diContainer;
    private Contexts _contexts;

    [Inject]
    public void Inject(
      DiContainer diContainer,
      Contexts contexts)
    {
      _diContainer = diContainer;
      _contexts = contexts;
    }

    public override void InstallBindings()
    {
      _diContainer.Bind<LocationGame>().FromInstance(LocationGame).AsSingle();
    }

    private void OnEnable()
    {
      CreateGameSystems();
    }

    private void OnDestroy()
    {
      _gameSystems.TearDown();
      _gameSystems.Execute();
      _gameSystems.Cleanup();
      _gameSystems.DeactivateReactiveSystems();

      _contexts.game.DestroyAllEntities();
      _contexts.game.ResetCreationIndex();
      _contexts.game.Reset();
    }

    private void CreateGameSystems()
    {
      _gameSystems = new InjectableFeature("GameSystems");

      CreateLoadSystems(_contexts);
      _gameSystems.IncjectSelfAndChildren(_diContainer);
      _gameSystems.Initialize();

      void CreateLoadSystems(Contexts contexts)
      {
        _gameSystems.Add(new CreatePlacementAreaSystem(contexts));
        _gameSystems.Add(new CreateHandSystem(contexts));
        _gameSystems.Add(new FillHandWithCardsSystem(contexts));
        _gameSystems.Add(new CreateUpdaterParametersOfCardsSystem(contexts));

        //Execute-->
        _gameSystems.Add(new UpdateTranslationSelectedCardSystem(contexts));
        _gameSystems.Add(new PreparePlacementCardSystem(contexts));
        //<--

        _gameSystems.Add(new DropCardOnPlacementAreaSystem(contexts));
        _gameSystems.Add(new PlacementCardSystem(contexts));
        _gameSystems.Add(new PlacementAreaInteractSystem(contexts));

        _gameSystems.Add(new TimerSystem(contexts));
        _gameSystems.Add(new AttackTimerSystem(contexts));
        _gameSystems.Add(new HealthTimerSystem(contexts));
        _gameSystems.Add(new CostTimerSystem(contexts));
        
        _gameSystems.Add(new LoadCardAssetSystem(contexts));
        _gameSystems.Add(new LoadCardTextureFromURLSystem(contexts));

        _gameSystems.Add(new ReleaseFirstStackCardsSystem(contexts));

        _gameSystems.Add(new DestroyCardWithLowHealthSystem(contexts));

        _gameSystems.Add(new UpdateCurrentCardsSystem(contexts));
        _gameSystems.Add(new UpdateCardIndexSystem(contexts));
        _gameSystems.Add(new CalculateCardPositionInHandSystem(contexts));
        _gameSystems.Add(new UpdateCardParameterSystem(contexts));

        // TearDown
        _gameSystems.Add(new ReleaseAddressablesAssetsSystem(contexts));
        _gameSystems.Add(new DestroyViewsSystem(contexts));

        // Generated
        _gameSystems.Add(new GameEventSystems(contexts));

        // CleanupSystem
        _gameSystems.Add(new DestroyDestroyedGameSystem(contexts));
        _gameSystems.Add(new DestroyDestroyedTimerSystem(contexts));
      }
    }

    private void Update()
    {
      _gameSystems.Execute();
      _gameSystems.Cleanup();
    }
  }
}