//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public DropCardsComponent dropCards { get { return (DropCardsComponent)GetComponent(GameComponentsLookup.DropCards); } }
    public bool hasDropCards { get { return HasComponent(GameComponentsLookup.DropCards); } }

    public void AddDropCards(System.Collections.Generic.List<GameEntity> newEntities) {
        var index = GameComponentsLookup.DropCards;
        var component = (DropCardsComponent)CreateComponent(index, typeof(DropCardsComponent));
        component.entities = newEntities;
        AddComponent(index, component);
    }

    public void ReplaceDropCards(System.Collections.Generic.List<GameEntity> newEntities) {
        var index = GameComponentsLookup.DropCards;
        var component = (DropCardsComponent)CreateComponent(index, typeof(DropCardsComponent));
        component.entities = newEntities;
        ReplaceComponent(index, component);
    }

    public void RemoveDropCards() {
        RemoveComponent(GameComponentsLookup.DropCards);
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

    static Entitas.IMatcher<GameEntity> _matcherDropCards;

    public static Entitas.IMatcher<GameEntity> DropCards {
        get {
            if (_matcherDropCards == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.DropCards);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDropCards = matcher;
            }

            return _matcherDropCards;
        }
    }
}
