
public static class GameContextExtension
{
  public static GameEntity CreateCard(this GameContext context,
    string assetName)
  {
    var ent = context.CreateEntity();
    ent.isCard = true;
    ent.AddAssetName(assetName);
    ent.isAsset = true;
    ent.AddAttack(6);
    ent.AddHealth(6);
    ent.AddCost(6);
    return ent;
  }
}

