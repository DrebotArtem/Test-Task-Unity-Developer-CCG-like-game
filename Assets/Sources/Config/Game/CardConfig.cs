using UnityEngine;

namespace DrebotGS.Config
{
  /// <summary>
  /// Main game configuration
  /// </summary>
  [CreateAssetMenu(fileName = "New CardConfig", menuName = "DrebotGS/Configs/CardConfig")]
  public class CardConfig : ScriptableObject
  {
    [Header("Image")]
    public string CardImageURL = "https://picsum.photos/1000/1000";

    [Header("Animations on start")]
    [Range(0.01f, 6)]
    public float AnimationDuration = 1;
    public Vector3 OffsetPositionToShow;
    public Vector3 OffsetRotationToShow;

    [Header("Animations on select")]
    [Range(0.01f, 6)]
    public float animationDurationToSelect = 0.1f;
    [Range(0.01f, 6)]
    public float animationDurationToUnselect = 0.4f;
    public Vector3 offsetPositionToSelect;
    public Vector3 setRotationToSelect;

    [Header("Placement in hand")]
    public float Angle = 0.5f;
    public float Distance = 1;
    public float FrontOffset = -0.01f;

    [Header("UI")]
    public int startSortingOrder = 100;
  }
}