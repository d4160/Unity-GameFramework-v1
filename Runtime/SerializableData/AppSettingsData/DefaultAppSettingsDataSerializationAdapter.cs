namespace d4160.GameFramework
{
    using d4160.Systems.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public class DefaultAppSettingsDataSerializationAdapter : AppSettingsDataSerializationAdapter<DefaultAppSettingsSerializableData, DefaultConcreteAppSettingsSerializableData>
    {
        public DefaultAppSettingsDataSerializationAdapter(DataSerializationAdapterType dataType = DataSerializationAdapterType.Generic) : base(dataType)
        {
        }

        protected override void FillFromSerializableDataForConcreteType(DefaultConcreteAppSettingsSerializableData data)
        {
            if (data == null)
            {
                Debug.LogWarning($"ConcreteAppSettingsSerializableData is null. ");
                return;
            }

            var settings = GameFrameworkSettings.AppSettingsDatabase.Settings;

            for (int i = 0; i < settings.Length; i++)
            {
                switch(i)
                {
                    case 0: settings[i].FillFromSerializableData(data.AppStatsSettingsData); break;
                    case 1: settings[i].FillFromSerializableData(data.AudioSettingsData); break;
                    case 2: settings[i].FillFromSerializableData(data.GraphicsSettingsData); break;
                    case 3: settings[i].FillFromSerializableData(data.LocalizationSettingsData); break;
                    case 4: settings[i].FillFromSerializableData(data.DPostProcessingSettingsData); break;
                }
            }
        }

        protected override ISerializableData GetSerializableDataForConcreteType(DefaultConcreteAppSettingsSerializableData data)
        {
            var settings = GameFrameworkSettings.AppSettingsDatabase.Settings;

            for (int i = 0; i < settings.Length; i++)
            {
                switch(i)
                {
                    case 0: data.AppStatsSettingsData = (settings[i].GetSerializableData() as DefaultAppStatsSettingsSerializableData); break;
                    case 1: data.AudioSettingsData = (settings[i].GetSerializableData() as DefaultAudioSettingsSerializableData); break;
                    case 2: data.GraphicsSettingsData = (settings[i].GetSerializableData() as DefaultGraphicsSettingsSerializableData); break;
                    case 3: data.LocalizationSettingsData = (settings[i].GetSerializableData() as DefaultLocalizationSettingsSerializableData); break;
                    case 4: data.DPostProcessingSettingsData = (settings[i].GetSerializableData() as DefaultPostProcessingSettingsSerializableData); break;
                }
            }

            return data;
        }
    }
}