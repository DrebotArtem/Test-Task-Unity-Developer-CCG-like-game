//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly ReadyToDropComponent readyToDropComponent = new ReadyToDropComponent();

    public bool isReadyToDrop {
        get { return HasComponent(GameComponentsLookup.ReadyToDrop); }
        set {
            if (value != isReadyToDrop) {
                var index = GameComponentsLookup.ReadyToDrop;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : readyToDropComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherReadyToDrop;

    public static Entitas.IMatcher<GameEntity> ReadyToDrop {
        get {
            if (_matcherReadyToDrop == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ReadyToDrop);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherReadyToDrop = matcher;
            }

            return _matcherReadyToDrop;
        }
    }
}
