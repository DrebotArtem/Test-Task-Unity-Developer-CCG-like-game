using UnityEngine;

namespace DrebotGS.Config
{
  /// <summary>
  /// Main game configuration
  /// </summary>
  [CreateAssetMenu(fileName = "New GameConfig", menuName = "DrebotGS/Configs/GameConfig")]
  public class GameConfig : ScriptableObject
  {
    public string CardImageURL = "https://picsum.photos/1000/1000";
  }
}