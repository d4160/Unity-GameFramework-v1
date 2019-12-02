namespace d4160.GameFramework
{
    using Malee;
    using System.Linq;
    using UnityEngine.GameFoundation;

    //[CreateAssetMenu(fileName = "New DefaultPlayers_SO.asset", menuName = "Game Framework/Game Data/Players")]
    public abstract class DefaultPlayersSO<T1, T2, T3> : ArchetypesSOBase<T1, T2, T3>
        where T1 : ReorderableArray<T2>
        where T2 : IArchetype, new()
        where T3 : BaseSerializableData
    {
    }
}