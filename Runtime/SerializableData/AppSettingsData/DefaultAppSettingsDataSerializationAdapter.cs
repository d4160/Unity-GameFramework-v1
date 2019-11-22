namespace d4160.GameFramework
{
    using d4160.Core;
    using d4160.Systems.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public class DefaultAppSettingsDataSerializationAdapter : IDataSerializationAdapter
    {
        protected DataSerializationAdapterType m_dataType;

        public DefaultAppSettingsDataSerializationAdapter(DataSerializationAdapterType dataType = DataSerializationAdapterType.Generic)
        {
            m_dataType = dataType;
        }

        public virtual void FillFromSerializableData(ISerializableData data)
        {
            switch(m_dataType)
            {
                case DataSerializationAdapterType.Generic:
                    var gData = data as DefaultAppSettingsSerializableData;

                    if (gData == null || gData.SettingsData == null)
                    {
                        Debug.LogWarning($"AppSettingsSerializableData is null. ");
                        return;
                    }

                    var settings = GameFrameworkSettings.AppSettingsDatabase.Settings;
                    for (int i = 0; i < gData.SettingsData.Length; i++)
                    {
                        if (!settings.IsValidIndex(i)) break;

                        settings[i].FillFromSerializableData(gData.SettingsData[i]);
                    }
                break;
                case DataSerializationAdapterType.Concrete:
                    var cData = data as DefaultConcreteAppSettingsSerializableData;

                    if (cData == null)
                    {
                        Debug.LogWarning($"ConcreteAppSettingsSerializableData is null. ");
                        return;
                    }

                    settings = GameFrameworkSettings.AppSettingsDatabase.Settings;

                    for (int i = 0; i < settings.Length; i++)
                    {
                        switch(i)
                        {
                            case 0: settings[i].FillFromSerializableData(cData.DefaultAppStatsSettingsData); break;
                            case 1: settings[i].FillFromSerializableData(cData.DefaultAudioSettingsData); break;
                            case 2: settings[i].FillFromSerializableData(cData.DefaultGraphicsSettingsData); break;
                            case 3: settings[i].FillFromSerializableData(cData.DefaultLocalizationSettingsData); break;
                            case 4: settings[i].FillFromSerializableData(cData.DefaultPostProcessingSettingsData); break;
                        }
                    }
                break;
            }
        }

        public virtual ISerializableData GetSerializableData()
        {
            switch(m_dataType)
            {
                case DataSerializationAdapterType.Generic:
                    var data = new DefaultAppSettingsSerializableData();
                    var settings = GameFrameworkSettings.AppSettingsDatabase.Settings;
                    var settingsData = new BaseSettingsSerializableData[settings.Length];

                    for (int i = 0; i < settings.Length; i++)
                    {
                        settingsData[i] = settings[i].GetSerializableData() as BaseSettingsSerializableData;
                    }

                    data.SettingsData = settingsData;

                    return data;
                case DataSerializationAdapterType.Concrete:
                    var cData = new DefaultConcreteAppSettingsSerializableData();
                    settings = GameFrameworkSettings.AppSettingsDatabase.Settings;

                    for (int i = 0; i < settings.Length; i++)
                    {
                        switch(i)
                        {
                            case 0: cData.DefaultAppStatsSettingsData = (settings[i].GetSerializableData() as DefaultAppStatsSettingsSerializableData); break;
                            case 1: cData.DefaultAudioSettingsData = (settings[i].GetSerializableData() as DefaultAudioSettingsSerializableData); break;
                            case 2: cData.DefaultGraphicsSettingsData = (settings[i].GetSerializableData() as DefaultGraphicsSettingsSerializableData); break;
                            case 3: cData.DefaultLocalizationSettingsData = (settings[i].GetSerializableData() as DefaultLocalizationSettingsSerializableData); break;
                            case 4: cData.DefaultPostProcessingSettingsData = (settings[i].GetSerializableData() as DefaultPostProcessingSettingsSerializableData); break;
                        }
                    }

                    return cData;

                default:
                    return null;
            }
        }

        public virtual void InitializeData(ISerializableData data)
        {
            if (data != null)
            {
                FillFromSerializableData(data);
            }
        }

        public virtual void Initialize(
            string saveDataPathFull,
            IDataPersistence dataPersistence,
            System.Action onInitializeCompleted = null,
            System.Action onInitializeFailed = null)
        {
            switch(m_dataType)
            {
                case DataSerializationAdapterType.Generic:
                    GameFramework.SetAppSettingsDataPath(saveDataPathFull);
                    // tell Game Framework to initialize using this
                    // persistence system. Only call Initialize once per session.
                    GameFramework.InitializeAppSettingsData<DefaultAppSettingsSerializableData>(dataPersistence, this, onInitializeCompleted, onInitializeFailed);
                break;
                case DataSerializationAdapterType.Concrete:
                    GameFramework.SetAppSettingsDataPath(saveDataPathFull);
                    // tell Game Framework to initialize using this
                    // persistence system. Only call Initialize once per session.
                    GameFramework.InitializeAppSettingsData<DefaultConcreteAppSettingsSerializableData>(dataPersistence, this, onInitializeCompleted, onInitializeFailed);
                break;
            }
        }

        public virtual void Load(
            IDataPersistence dataPersistence,
            System.Action onLoadCompleted = null,
            System.Action onLoadFailed = null)
        {
            switch(m_dataType)
            {
                case DataSerializationAdapterType.Generic:
                    GameFramework.LoadAppSettingsData<DefaultAppSettingsSerializableData>(dataPersistence, onLoadCompleted, onLoadFailed);
                break;
                case DataSerializationAdapterType.Concrete:
                    GameFramework.LoadAppSettingsData<DefaultConcreteAppSettingsSerializableData>(dataPersistence, onLoadCompleted, onLoadFailed);
                break;
            }
        }

        public virtual void Save(
            IDataPersistence dataPersistence,
            System.Action onSaveCompleted = null,
            System.Action onSaveFailed = null)
        {
            switch(m_dataType)
            {
                case DataSerializationAdapterType.Generic:
                    GameFramework.SaveAppSettingsData(dataPersistence, onSaveCompleted, onSaveFailed);
                break;
                case DataSerializationAdapterType.Concrete:
                    GameFramework.SaveAppSettingsData(dataPersistence, onSaveCompleted, onSaveFailed);
                break;
            }
        }
    }
}