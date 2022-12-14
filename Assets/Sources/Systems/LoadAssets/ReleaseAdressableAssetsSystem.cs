using Entitas;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace DrebotGS.Systems
{
  public class ReleaseAddressablesAssetsSystem : ITearDownSystem
  {
    readonly IGroup<GameEntity> _groupLoadAsync;
    readonly List<GameEntity> _bufferLoadAsync = new List<GameEntity>();

    public ReleaseAddressablesAssetsSystem(Contexts contexts)
    {
      _groupLoadAsync = contexts.game.GetGroup(GameMatcher.LoadAsync);
    }

    public void TearDown()
    {
      foreach (var e in _groupLoadAsync.GetEntities(_bufferLoadAsync))
        Addressables.Release(e.loadAsync.value);
    }
  }
}