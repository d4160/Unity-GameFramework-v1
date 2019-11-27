namespace d4160.GameFramework
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New DefaultPlayers_SO.asset", menuName = "Game Framework/Game Data/Default Players")]
    public class DefaultPlayersSO : ReorderableSO<PlayersReorderableArray, DefaultPlayer>
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