namespace d4160.GameFramework
{
    using System.Linq;
    using UnityEngine;
    using UnityEngine.GameFoundation;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New DefaultPlayers_SO.asset", menuName = "Game Framework/Game Data/Default Players")]
    public class PlayTrialsSO : ArchetypesSOBase<LeaderboardsReorderableArray, DefaultLeaderboard>
    {
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