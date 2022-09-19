using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace DrebotGS.UI
{
  [RequireComponent(typeof(UIDocument))]
  public class GameplayScreenMono : MonoBehaviour
  {
    private InputMasterControls _inputControl;

    private VisualElement _rootVisualElement;
    private const string _titleUpdateCardButtonName = "update-card-button";
    private const string _titleRestartGameButtonName = "restart-button";
    private const string _titleExitButtonName = "exit-button";

    //Inject
    private LoadingSceneHelper _loadingSceneHelper;
    private Contexts _contexts;
    [Inject]
    public void Inject(Contexts contexts, LoadingSceneHelper loadingSceneHelper)
    {
      _contexts = contexts;
      _loadingSceneHelper = loadingSceneHelper;
    }

    public void Awake()
    {
      InitInputControl();
      InitVisualElements();
      BindGameplayButtons();
    }

    private void InitInputControl()
    {
      _inputControl = new InputMasterControls();
      _inputControl.Enable();
    }

    private void InitVisualElements()
    {
      _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
    }

    private void BindGameplayButtons()
    {
      BindUpdateCardButton();
      BindRestartGameButton();
      BindExitButton();

        void BindUpdateCardButton()
      {
        var updateCardParam = _rootVisualElement.Q<Button>(_titleUpdateCardButtonName);
        if (updateCardParam != null)
        {
          updateCardParam.clickable.clicked += () =>
          {
            var ent = _contexts.game.CreateEntity();
            ent.isUpdateCardParameter = true;
          };
        }
      }
      void BindRestartGameButton()
      {
        var restartGame = _rootVisualElement.Q<Button>(_titleRestartGameButtonName);
        if (restartGame != null)
        {
          restartGame.clickable.clicked += () =>
          {
            _loadingSceneHelper.LoadNewGameScene();
          };
        }
      }
      void BindExitButton()
      {
        var exitGame = _rootVisualElement.Q<Button>(_titleExitButtonName);
        if (exitGame != null)
        {
          exitGame.clickable.clicked += () =>
          {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
          };
        }
      }
    }
  }
}