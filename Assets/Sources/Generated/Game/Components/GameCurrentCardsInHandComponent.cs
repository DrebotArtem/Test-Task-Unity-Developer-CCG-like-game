//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CurrentCardsInHandComponent currentCardsInHand { get { return (CurrentCardsInHandComponent)GetComponent(GameComponentsLookup.CurrentCardsInHand); } }
    public bool hasCurrentCardsInHand { get { return HasComponent(GameComponentsLookup.CurrentCardsInHand); } }

    public void AddCurrentCardsInHand(int newValue) {
        var index = GameComponentsLookup.CurrentCardsInHand;
        var component = (CurrentCardsInHandComponent)CreateComponent(index, typeof(CurrentCardsInHandComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCurrentCardsInHand(int newValue) {
        var index = GameComponentsLookup.CurrentCardsInHand;
        var component = (CurrentCardsInHandComponent)CreateComponent(index, typeof(CurrentCardsInHandComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCurrentCardsInHand() {
        RemoveComponent(GameComponentsLookup.CurrentCardsInHand);
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

    static Entitas.IMatcher<GameEntity> _matcherCurrentCardsInHand;

    public static Entitas.IMatcher<GameEntity> CurrentCardsInHand {
        get {
            if (_matcherCurrentCardsInHand == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CurrentCardsInHand);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCurrentCardsInHand = matcher;
            }

            return _matcherCurrentCardsInHand;
        }
    }
}
