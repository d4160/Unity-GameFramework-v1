namespace d4160.GameFramework
{
    using System;
  using d4160.Systems.DataPersistence;
  using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public static class GameFramework<T> where T : ISerializableData
    {
        private enum InitializationStatus
        {
            NotInitialized,
            Initializing,
            Initialized,
            Failed
        }

        private static string m_AppSettingsDataPath;
        private static InitializationStatus m_AppSettingsInitializationStatus = InitializationStatus.NotInitialized;

        /// <summary>
        /// Check if the AppSettings is initialized.
        /// </summary>
        /// <returns>Whether the AppSettings is initialized or not</returns>
        public static bool IsInitialized
        {
            get { return m_AppSettingsInitializationStatus == InitializationStatus.Initialized; }
        }

        public static void SetAppSettingsDataPath(string dataPath) => m_AppSettingsDataPath = dataPath;

        /// <summary>
        /// Initialize the GameFoundation . It need a persistence object to be passed as argument to set the default persistence layer
        /// If the initialization fails, onInitializeFailed will be called with an exception.
        /// </summary>
        /// <param name="dataPersistence">The persistence layer of the Game Foundation. Required and cached for future execution</param>
        /// <param name="onInitializeCompleted">Called when the initialization process is completed with success</param>
        /// <param name="onInitializeFailed">Called when the initialization process failed</param>
        public static void InitializeAppSettings(
            IDataPersistence dataPersistence = null,
            IDataSerializationAdapter dataAdapter = null,
            Action onInitializeCompleted = null,
            Action onInitializeFailed = null)
        {
            if (m_AppSettingsInitializationStatus == InitializationStatus.Initializing ||
                m_AppSettingsInitializationStatus == InitializationStatus.Initialized)
            {
                Debug.LogWarning("AppSettings is already initialized and cannot be initialized again.");
                onInitializeFailed?.Invoke();

                return;
            }

            m_AppSettingsInitializationStatus = InitializationStatus.Initializing;

            if (dataPersistence != null && dataAdapter != null)
            {
                LoadAppSettingsData(dataPersistence,
                    (data) =>
                    {
                        InitializeAppSettings(data, dataAdapter, onInitializeCompleted, onInitializeFailed);
                    },
                    () =>
                    {
                        InitializeAppSettings(null as ISerializableData, dataAdapter, onInitializeCompleted, onInitializeFailed);
                    });
            }
            else
            {
                InitializeAppSettings(null as ISerializableData, dataAdapter, onInitializeCompleted, onInitializeFailed);
            }
        }

        static void InitializeAppSettings(ISerializableData data, IDataSerializationAdapter dataAdapter, Action onInitializeCompleted = null, Action onInitializeFailed = null)
        {
            bool isInitialized = true;

            try
            {
                AppSettings.Database.DataAdapter = dataAdapter;
                AppSettings.Database.Initialize(data);

                isInitialized = true;
            }
            catch
            {
                isInitialized = false;
            }

            if (isInitialized)
            {
                m_AppSettingsInitializationStatus = InitializationStatus.Initialized;
                onInitializeCompleted?.Invoke();
            }
            else
            {
                UninitializeAppSettings();

                Debug.LogWarning("AppSettings can't be initialized.");

                m_AppSettingsInitializationStatus = InitializationStatus.Failed;
                onInitializeFailed?.Invoke();
            }
        }

        internal static void UninitializeAppSettings()
        {
            m_AppSettingsInitializationStatus = InitializationStatus.NotInitialized;

            AppSettings.Database.Unintialize();
        }

        /// <summary>
        /// Asynchronously loads data from the persistence layer and populates managed systems with it
        /// </summary>
        /// <param name="dataPersistence">The persistence layer used to execute the process</param>
        /// <param name="onLoadCompleted">Called when the loading process is completed with success</param>
        /// <param name="onLoadFailed">Called when the loading process failed</param>
        public static void LoadAppSettings(
            IDataPersistence dataPersistence,
            Action onLoadCompleted = null,
            Action onLoadFailed = null)
        {
            if (dataPersistence == null)
            {
                onLoadFailed?.Invoke();
                Debug.LogWarning("DataPersistence cannot be null on persistence process.");
                return;
            }

            LoadAppSettingsData(dataPersistence,
                (data) =>
                {
                    try
                    {
                        FillAppSettings(data);
                        onLoadCompleted?.Invoke();
                    }
                    catch
                    {
                        onLoadFailed?.Invoke();
                    }

                }, onLoadFailed);
        }

        private static void LoadAppSettingsData
        (
            IDataPersistence dataPersistence,
            Action<ISerializableData> onLoadCompleted = null,
            Action onLoadFailed = null)
        {
            if (dataPersistence == null)
            {
                onLoadFailed?.Invoke();
                Debug.LogWarning("DataPersistence cannot be null on persistence process.");
            }

            dataPersistence.Load<T>(m_AppSettingsDataPath,
                (data) =>
                {
                    try
                    {
                        onLoadCompleted?.Invoke(data);
                    }
                    catch
                    {
                        onLoadFailed?.Invoke();
                    }
                },
                () => { onLoadFailed?.Invoke(); });
        }

        private static void FillAppSettings(ISerializableData data)
        {
            AppSettings.Database.FillFromSerializableData(data);
        }

        /// <summary>
        /// Asynchronously saves data through the persistence layer.
        /// </summary>
        /// <param name="dataPersistence">The persistence layer used to execute the process</param>
        /// <param name="onSaveCompleted">Called when the saving process is completed with success</param>
        /// <param name="onSaveFailed">Called when the saving process failed</param>
        public static void SaveAppSettings(
            IDataPersistence dataPersistence,
            Action onSaveCompleted = null,
            Action onSaveFailed = null)
        {
            if (dataPersistence == null)
            {
                onSaveFailed?.Invoke();
                Debug.LogWarning("DataPersistence cannot be null on persistence process.");
                return;
            }

            if (!AppSettings.Database.IsInitialized)
            {
                onSaveFailed?.Invoke();
                Debug.LogWarning("Cannot save AppSettings. AppSettings is not initialized.");
                return;
            }

            var appSettingsData = AppSettings.Database.GetSerializableData();
            dataPersistence.Save(m_AppSettingsDataPath, appSettingsData, onSaveCompleted, onSaveFailed);
        }
    }
}