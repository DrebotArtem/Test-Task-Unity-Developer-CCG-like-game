using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public sealed class UpdaterParametersOfCardsComponent : IComponent
{
  public int currentCardIndex;
}