//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PlayerHandEntityReferenceComponent playerHandEntityReference { get { return (PlayerHandEntityReferenceComponent)GetComponent(GameComponentsLookup.PlayerHandEntityReference); } }
    public bool hasPlayerHandEntityReference { get { return HasComponent(GameComponentsLookup.PlayerHandEntityReference); } }

    public void AddPlayerHandEntityReference(GameEntity newValue) {
        var index = GameComponentsLookup.PlayerHandEntityReference;
        var component = (PlayerHandEntityReferenceComponent)CreateComponent(index, typeof(PlayerHandEntityReferenceComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePlayerHandEntityReference(GameEntity newValue) {
        var index = GameComponentsLookup.PlayerHandEntityReference;
        var component = (PlayerHandEntityReferenceComponent)CreateComponent(index, typeof(PlayerHandEntityReferenceComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePlayerHandEntityReference() {
        RemoveComponent(GameComponentsLookup.PlayerHandEntityReference);
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

    static Entitas.IMatcher<GameEntity> _matcherPlayerHandEntityReference;

    public static Entitas.IMatcher<GameEntity> PlayerHandEntityReference {
        get {
            if (_matcherPlayerHandEntityReference == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PlayerHandEntityReference);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPlayerHandEntityReference = matcher;
            }

            return _matcherPlayerHandEntityReference;
        }
    }
}
