using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Event(EventTarget.Self)]
public sealed class TranslationComponent : IComponent
{
  public Vector3 value;
}