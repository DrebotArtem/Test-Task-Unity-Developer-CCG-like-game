using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public sealed class CameraComponent : IComponent
{
  public Camera value;
}