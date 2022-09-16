using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Event(EventTarget.Self)]
public sealed class CardTexture2DComponent : IComponent
{
  public Texture2D value;
}