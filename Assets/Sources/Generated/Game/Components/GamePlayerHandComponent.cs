//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly PlayerHandComponent playerHandComponent = new PlayerHandComponent();

    public bool isPlayerHand {
        get { return HasComponent(GameComponentsLookup.PlayerHand); }
        set {
            if (value != isPlayerHand) {
                var index = GameComponentsLookup.PlayerHand;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : playerHandComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherPlayerHand;

    public static Entitas.IMatcher<GameEntity> PlayerHand {
        get {
            if (_matcherPlayerHand == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PlayerHand);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPlayerHand = matcher;
            }

            return _matcherPlayerHand;
        }
    }
}
