namespace d4160.GameFramework
{
    using Malee;

    //[CreateAssetMenu(fileName = "New Archetypes_SO.asset", menuName = "Game Framework/Game Data/Archetypes")]
    public abstract class DefaultArchetypesSO<T1, T2, T3> : ArchetypesSOBase<T1, T2, T3>
        where T1 : ReorderableArray<T2>
        where T2 : IArchetype, new()
        where T3 : BaseSerializableData
    {

    }
}