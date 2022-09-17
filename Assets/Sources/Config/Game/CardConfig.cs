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

    [Header("Animations")]
    [Range(0.01f, 6)]
    public float AnimationDuration = 1;
    public Vector3 OffsetPositionToShow;
    public Vector3 OffsetRotationToShow;
  }
}