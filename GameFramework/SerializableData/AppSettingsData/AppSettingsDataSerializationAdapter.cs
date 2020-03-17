namespace d4160.GameFramework
{
    using d4160.Core;
    using d4160.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public abstract class AppSettingsDataSerializationAdapter<T1, T2> : IDataSerializationAdapter where T1 : class, IGenericSerializableData, new() where T2 : class, ISerializableData, new()
    {
        protected DataSerializationAdapterType m_dataType;

        public AppSettingsDataSerializationAdapter(DataSerializationAdapterType dataType = DataSerializationAdapterType.Generic)
        {
            m_dataType = dataType;
        }

        public virtual void FillFromSerializableData(ISerializableData data)
        {
            switch(m_dataType)
            {
                case DataSerializationAdapterType.Generic:
                    FillFromSerializableDataForGenericType(data as T1);
                break;
                case DataSerializationAdapterType.Concrete:
                    FillFromSerializableDataForConcreteType(data as T2);
                break;
            }
        }

        protected virtual void FillFromSerializableDataForGenericType(T1 data)
        {
            if (data == null || data.SerializableData == null)
            {
                Debug.LogWarning($"AppSettingsSerializableData is null. ");
                return;
            }

            var settings = GameFrameworkSettings.AppSettingsDatabase.AppSettingsData;
            for (int i = 0; i < data.SerializableData.Length; i++)
            {
                if (!settings.IsValidIndex(i)) break;

                settings[i].FillFromSerializableData(data.SerializableData[i]);
            }
        }

        protected virtual void FillFromSerializableDataForConcreteType(T2 data)
        {
        }

        public virtual ISerializableData GetSerializableData()
        {
            switch(m_dataType)
            {
                case DataSerializationAdapterType.Generic:
                    var data = new T1();
                    return GetSerializableDataForGenericType(data);
                case DataSerializationAdapterType.Concrete:
                    var cData = new T2();
                    return GetSerializableDataForConcreteType(cData);
                default:
                    return null;
            }
        }

        protected virtual ISerializableData GetSerializableDataForGenericType(T1 data)
        {
            var settings = GameFrameworkSettings.AppSettingsDatabase.AppSettingsData;
            var serializableData = new BaseSerializableData[settings.Length];

            for (int i = 0; i < settings.Length; i++)
            {
                serializableData[i] = settings[i].GetSerializableData() as BaseSerializableData;
            }

            data.SerializableData = serializableData;

            return data;
        }

        protected virtual ISerializableData GetSerializableDataForConcreteType(T2 data)
        {
            return null;
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
                    GameFramework.InitializeAppSettingsData<T1>(dataPersistence, this, onInitializeCompleted, onInitializeFailed);
                break;
                case DataSerializationAdapterType.Concrete:
                    GameFramework.SetAppSettingsDataPath(saveDataPathFull);
                    // tell Game Framework to initialize using this
                    // persistence system. Only call Initialize once per session.
                    GameFramework.InitializeAppSettingsData<T2>(dataPersistence, this, onInitializeCompleted, onInitializeFailed);
                break;
            }
        }

        public virtual void Deinitialize()
        {
            GameFramework.UninitializeAppSettingsData();
        }

        public virtual void Load(
            IDataPersistence dataPersistence,
            System.Action onLoadCompleted = null,
            System.Action onLoadFailed = null)
        {
            switch(m_dataType)
            {
                case DataSerializationAdapterType.Generic:
                    GameFramework.LoadAppSettingsData<T1>(dataPersistence, onLoadCompleted, onLoadFailed);
                break;
                case DataSerializationAdapterType.Concrete:
                    GameFramework.LoadAppSettingsData<T2>(dataPersistence, onLoadCompleted, onLoadFailed);
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