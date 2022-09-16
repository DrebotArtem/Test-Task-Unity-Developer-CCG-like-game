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
    private LoadingSceneHelper _loadingSceneHelper;

    [Inject]
    public void Inject(
      DiContainer diContainer,
      Contexts contexts,
      LoadingSceneHelper loadingSceneHelper)
    {
      _diContainer = diContainer;
      _contexts = contexts;
      _loadingSceneHelper = loadingSceneHelper;
    }

    public override void InstallBindings()
    {
      _diContainer.BindInterfacesAndSelfTo<LocationGame>().FromInstance(LocationGame).AsSingle();
    }

    public override void Start()
    {
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
        _gameSystems.Add(new CreateCardSystem(contexts));

        _gameSystems.Add(new LoadCardAssetSystem(contexts));
        _gameSystems.Add(new LoadCardTextureFromURLSystem(contexts));

        // TearDown
        _gameSystems.Add(new ReleaseAddressablesAssetsSystem(contexts));
        _gameSystems.Add(new DestroyViewsSystem(contexts));

        // Generated
        _gameSystems.Add(new GameEventSystems(contexts));

        // CleanupSystem
        _gameSystems.Add(new DestroyDestroyedGameSystem(contexts));
      }
    }

    private void Update()
    {
      _gameSystems.Execute();
      _gameSystems.Cleanup();
    }
  }
}