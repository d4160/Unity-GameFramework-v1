using d4160.Core;

namespace d4160.GameFramework
{
    using Malee;
    using System.Linq;
    using UnityEngine.GameFoundation;
    using d4160.DataPersistence;

    //[CreateAssetMenu(fileName = "New Leaderboards_SO.asset", menuName = "Game Framework/Game Data/Leaderboards")]
    public abstract class DefaultLeaderboardsSO<T1, T2, T3> : ArchetypesSOBase<T1, T2, T3>
        where T1 : ReorderableArray<T2>
        where T2 : IArchetype, IArchetypeName, new()
        where T3 : BaseSerializableData
    {
#if UNITY_EDITOR
        protected string[] StatNames => StatManager.catalog.GetStatDefinitions().Select((x) => x.displayName).ToArray();
#endif
    }
}