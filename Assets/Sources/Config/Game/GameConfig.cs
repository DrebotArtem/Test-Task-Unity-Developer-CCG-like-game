using UnityEngine;

namespace DrebotGS.Config
{
  /// <summary>
  /// Main game configuration
  /// </summary>
  [CreateAssetMenu(fileName = "New GameConfig", menuName = "DrebotGS/Configs/GameConfig")]
  public class GameConfig : ScriptableObject
  {
    [Range(1, 10)]
    [SerializeField] private int minCardsOnStart = 4;

    [Range(1, 10)]
    [SerializeField] private int maxCardsOnStart = 6;

    public int MaxCardsOnStart
    {
      get 
      {
        if (MinCardsOnStart > maxCardsOnStart)
          return MinCardsOnStart;
        return maxCardsOnStart;
      }
    }

    public int MinCardsOnStart
    {
      get
      {
        if (minCardsOnStart < 1)
          return 1;
        else
          return minCardsOnStart;
      }
    }
  }
}