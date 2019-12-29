namespace d4160.GameFramework
{
    using d4160.Core;

    public interface IArchetypeGetter<TArchetype> : IElementGetter<TArchetype> where TArchetype : IArchetype
    {
    }
}
