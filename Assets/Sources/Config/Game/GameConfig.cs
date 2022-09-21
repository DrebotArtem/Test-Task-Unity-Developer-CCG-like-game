using UnityEngine;

namespace DrebotGS.Config
{
  /// <summary>
  /// Main game configuration
  /// </summary>
  [CreateAssetMenu(fileName = "New GameConfig", menuName = "DrebotGS/Configs/GameConfig")]
  public class GameConfig : ScriptableObject
  {
    [Header("Cards on start")]
    [Range(1, 10)]
    [SerializeField] private int minCardsOnStart = 4;

    [Range(1, 10)]
    [SerializeField] private int maxCardsOnStart = 6;

    [Header("Placement Areat")]
    public LayerMask placementAreaLayer;
    public Color placementAreaBaseColor;
    public Color placementAreaInteractiveColor;
    [Range(0.5f, 1.5f)]
    public float distanceBetweenCards = 1.2f;
    [Range(0.1f, 1.5f)]
    public float offsetY = 0.3f;

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