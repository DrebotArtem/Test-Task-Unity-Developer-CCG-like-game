//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TimerEntity {

    public EntitiesReferenceComponent entitiesReference { get { return (EntitiesReferenceComponent)GetComponent(TimerComponentsLookup.EntitiesReference); } }
    public bool hasEntitiesReference { get { return HasComponent(TimerComponentsLookup.EntitiesReference); } }

    public void AddEntitiesReference(System.Collections.Generic.List<GameEntity> newGameEntities) {
        var index = TimerComponentsLookup.EntitiesReference;
        var component = (EntitiesReferenceComponent)CreateComponent(index, typeof(EntitiesReferenceComponent));
        component.gameEntities = newGameEntities;
        AddComponent(index, component);
    }

    public void ReplaceEntitiesReference(System.Collections.Generic.List<GameEntity> newGameEntities) {
        var index = TimerComponentsLookup.EntitiesReference;
        var component = (EntitiesReferenceComponent)CreateComponent(index, typeof(EntitiesReferenceComponent));
        component.gameEntities = newGameEntities;
        ReplaceComponent(index, component);
    }

    public void RemoveEntitiesReference() {
        RemoveComponent(TimerComponentsLookup.EntitiesReference);
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
public sealed partial class TimerMatcher {

    static Entitas.IMatcher<TimerEntity> _matcherEntitiesReference;

    public static Entitas.IMatcher<TimerEntity> EntitiesReference {
        get {
            if (_matcherEntitiesReference == null) {
                var matcher = (Entitas.Matcher<TimerEntity>)Entitas.Matcher<TimerEntity>.AllOf(TimerComponentsLookup.EntitiesReference);
                matcher.componentNames = TimerComponentsLookup.componentNames;
                _matcherEntitiesReference = matcher;
            }

            return _matcherEntitiesReference;
        }
    }
}
