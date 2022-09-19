//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TimerEntity {

    public EntityReferenceComponent entityReference { get { return (EntityReferenceComponent)GetComponent(TimerComponentsLookup.EntityReference); } }
    public bool hasEntityReference { get { return HasComponent(TimerComponentsLookup.EntityReference); } }

    public void AddEntityReference(GameEntity newGameEntity) {
        var index = TimerComponentsLookup.EntityReference;
        var component = (EntityReferenceComponent)CreateComponent(index, typeof(EntityReferenceComponent));
        component.gameEntity = newGameEntity;
        AddComponent(index, component);
    }

    public void ReplaceEntityReference(GameEntity newGameEntity) {
        var index = TimerComponentsLookup.EntityReference;
        var component = (EntityReferenceComponent)CreateComponent(index, typeof(EntityReferenceComponent));
        component.gameEntity = newGameEntity;
        ReplaceComponent(index, component);
    }

    public void RemoveEntityReference() {
        RemoveComponent(TimerComponentsLookup.EntityReference);
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

    static Entitas.IMatcher<TimerEntity> _matcherEntityReference;

    public static Entitas.IMatcher<TimerEntity> EntityReference {
        get {
            if (_matcherEntityReference == null) {
                var matcher = (Entitas.Matcher<TimerEntity>)Entitas.Matcher<TimerEntity>.AllOf(TimerComponentsLookup.EntityReference);
                matcher.componentNames = TimerComponentsLookup.componentNames;
                _matcherEntityReference = matcher;
            }

            return _matcherEntityReference;
        }
    }
}