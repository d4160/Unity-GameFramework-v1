namespace d4160.GameFramework
{
    using d4160.Systems.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public class DefaultGameDataSerializationAdapter : GameDataSerializationAdapter<DefaultGameSerializableData, DefaultConcreteGameSerializableData>
    {
        public DefaultGameDataSerializationAdapter(DataSerializationAdapterType dataType = DataSerializationAdapterType.Generic) : base(dataType)
        {
        }

        protected override void FillFromSerializableDataForConcreteType(DefaultConcreteGameSerializableData data)
        {
            if (data == null)
            {
                Debug.LogWarning($"ConcreteGameSerializableData is null. ");
                return;
            }

            var gameData = GameFrameworkSettings.GameDatabase.GameData;

            for (int i = 0; i < gameData.Length; i++)
            {
                switch(i)
                {
                    //case 0: gameData[i].FillFromSerializableData(data.AppStatsSettingsData); break;
                    default:
                    break;
                }
            }
        }

        protected override ISerializableData GetSerializableDataForConcreteType(DefaultConcreteGameSerializableData data)
        {
            var gameData = GameFrameworkSettings.GameDatabase.GameData;

            for (int i = 0; i < gameData.Length; i++)
            {
                switch(i)
                {
                    //case 0: data.AppStatsSettingsData = (gameData[i].GetSerializableData() as DefaultAppStatsSettingsSerializableData); break;
                    default:
                    break;
                }
            }

            return data;
        }
    }
}