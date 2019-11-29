namespace d4160.GameFramework
{
    using System.Linq;
    using UnityEngine;
    using UnityEngine.GameFoundation;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New DefaultPlayTrials_SO.asset", menuName = "Game Framework/Game Data/Default Leaderboards")]
    public class DefaultPlayTrialsSO : ReorderableSO<DefaultPlayTrialsReorderableArray, DefaultPlayTrial>
    {
        public override void FillFromSerializableData(ISerializableData data)
        {
        }

        public override ISerializableData GetSerializableData()
        {
            return null;
        }
    }
}