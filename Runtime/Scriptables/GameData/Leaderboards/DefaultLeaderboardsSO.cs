namespace d4160.GameFramework
{
    using System.Linq;
    using UnityEngine;
    using UnityEngine.GameFoundation;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New DefaultLeaderboards_SO.asset", menuName = "Game Framework/Game Data/Default Leaderboards")]
    public class DefaultLeaderboardsSO : ArchetypesSOBase<LeaderboardsReorderableArray, DefaultLeaderboard>
    {
#if UNITY_EDITOR
        protected string[] StatNames => StatManager.catalog.GetStatDefinitions().Select((x) => x.id).ToArray();
#endif

        public override void FillFromSerializableData(ISerializableData data)
        {
        }

        public override ISerializableData GetSerializableData()
        {
            return null;
        }

        public override void InitializeData(ISerializableData data)
        {
            if(data != null)
                FillFromSerializableData(data);
        }
    }
}