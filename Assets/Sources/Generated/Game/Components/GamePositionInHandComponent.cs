//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PositionInHandComponent positionInHand { get { return (PositionInHandComponent)GetComponent(GameComponentsLookup.PositionInHand); } }
    public bool hasPositionInHand { get { return HasComponent(GameComponentsLookup.PositionInHand); } }

    public void AddPositionInHand(int newValue) {
        var index = GameComponentsLookup.PositionInHand;
        var component = (PositionInHandComponent)CreateComponent(index, typeof(PositionInHandComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePositionInHand(int newValue) {
        var index = GameComponentsLookup.PositionInHand;
        var component = (PositionInHandComponent)CreateComponent(index, typeof(PositionInHandComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePositionInHand() {
        RemoveComponent(GameComponentsLookup.PositionInHand);
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

    static Entitas.IMatcher<GameEntity> _matcherPositionInHand;

    public static Entitas.IMatcher<GameEntity> PositionInHand {
        get {
            if (_matcherPositionInHand == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PositionInHand);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPositionInHand = matcher;
            }

            return _matcherPositionInHand;
        }
    }
}
