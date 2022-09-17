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
    private const string _titleRestartGameButtonName = "restart-button";
    private const string _titleExitButtonName = "exit-button";

    //Inject
    private LoadingSceneHelper _loadingSceneHelper;

    [Inject]
    public void Inject(LoadingSceneHelper loadingSceneHelper)
    {
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
      BindRestartGameButton();
      BindExitButton();

      void BindRestartGameButton()
      {
        var newGame = _rootVisualElement.Q<Button>(_titleRestartGameButtonName);
        if (newGame != null)
        {
          newGame.clickable.clicked += () =>
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