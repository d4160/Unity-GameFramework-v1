using d4160.Core;

namespace d4160.GameFramework
{
    using Malee;
    using d4160.DataPersistence;

    //[CreateAssetMenu(fileName = "New DefaultPlayers_SO.asset", menuName = "Game Framework/Game Data/Players")]
    public abstract class DefaultPlayersSO<T1, T2, T3> : ArchetypesSOBase<T1, T2, T3>
        where T1 : ReorderableArray<T2>
        where T2 : IArchetype, IArchetypeName, new()
        where T3 : BaseSerializableData
    {
    }
}