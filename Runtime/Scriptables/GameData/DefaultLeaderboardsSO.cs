namespace d4160.GameFramework
{
    using Malee;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.GameFoundation;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New DefaultLeaderboards_SO.asset", menuName = "Game Framework/Game Data/Default Leaderboards")]
    public abstract class DefaultLeaderboardsSO<T1, T2, T3> : ArchetypesSOBase<T1, T2, T3>
    where T1 : ReorderableArray<T2>
    where T2 : IArchetype, new()
    where T3 : BaseSerializableData
    {
#if UNITY_EDITOR
        protected string[] StatNames => StatManager.catalog.GetStatDefinitions().Select((x) => x.id).ToArray();
#endif
    }
}