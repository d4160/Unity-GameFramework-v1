namespace d4160.GameFramework
{
    using d4160.Systems.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public class DefaultPlayerDataSerializationAdapter : PlayerDataSerializationAdapter<DefaultPlayerSerializableData, DefaultConcretePlayerSerializableData>
    {
        public DefaultPlayerDataSerializationAdapter(DataSerializationAdapterType dataType = DataSerializationAdapterType.Generic) : base(dataType)
        {
        }

        protected override void FillFromSerializableDataForConcreteType(DefaultConcretePlayerSerializableData data)
        {
            if (data == null)
            {
                Debug.LogWarning($"ConcretePlayerSerializableData is null. ");
                return;
            }

            var playerData = GameFrameworkSettings.PlayerDatabase.PlayerData;

            for (int i = 0; i < playerData.Length; i++)
            {
                switch(i)
                {
                    //case 0: PlayerData[i].FillFromSerializableData(data.AppStatsSettingsData); break;
                    default:
                    break;
                }
            }
        }

        protected override ISerializableData GetSerializableDataForConcreteType(DefaultConcretePlayerSerializableData data)
        {
            var playerData = GameFrameworkSettings.PlayerDatabase.PlayerData;

            for (int i = 0; i < playerData.Length; i++)
            {
                switch(i)
                {
                    //case 0: data.AppStatsSettingsData = (PlayerData[i].GetSerializableData() as DefaultAppStatsSettingsSerializableData); break;
                    default:
                    break;
                }
            }

            return data;
        }
    }
}