namespace d4160.GameFramework
{
    using System;
    using d4160.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public static class GameFramework
    {
        private enum InitializationStatus
        {
            NotInitialized,
            Initializing,
            Initialized,
            Failed
        }

        private static string m_AppSettingsDataPath;
        private static InitializationStatus m_AppSettingsDataInitializationStatus = InitializationStatus.NotInitialized;
        private static string m_GameDataPath;
        private static InitializationStatus m_GameDataInitializationStatus = InitializationStatus.NotInitialized;
        private static string m_PlayerDataPath;
        private static InitializationStatus m_PlayerDataInitializationStatus = InitializationStatus.NotInitialized;

        /// <summary>
        /// Check if the AppSettings data provider is initialized.
        /// </summary>
        /// <returns>Whether the AppSettings is initialized or not</returns>
        public static bool IsAppSettingsDataInitialized => m_AppSettingsDataInitializationStatus == InitializationStatus.Initialized;
        public static bool IsGameDataInitialized => m_GameDataInitializationStatus == InitializationStatus.Initialized;
        public static bool IsPlayerDataInitialized => m_PlayerDataInitializationStatus == InitializationStatus.Initialized;

        public static void SetAppSettingsDataPath(string dataPath) => m_AppSettingsDataPath = dataPath;
        public static void SetGameDataPath(string dataPath) => m_GameDataPath = dataPath;
        public static void SetPlayerDataPath(string dataPath) => m_PlayerDataPath = dataPath;

        #region AppSettings Data
        /// <summary>
        /// Initialize the GameFoundation . It need a persistence object to be passed as argument to set the default persistence layer
        /// If the initialization fails, onInitializeFailed will be called with an exception.
        /// </summary>
        /// <param name="dataPersistence">The persistence layer of the Game Foundation. Required and cached for future execution</param>
        /// <param name="onInitializeCompleted">Called when the initialization process is completed with success</param>
        /// <param name="onInitializeFailed">Called when the initialization process failed</param>
        public static void InitializeAppSettingsData<T>(
            IDataPersistence dataPersistence = null,
            IDataSerializationAdapter dataAdapter = null,
            Action onInitializeCompleted = null,
            Action onInitializeFailed = null) where T : ISerializableData
        {
            if (m_AppSettingsDataInitializationStatus == InitializationStatus.Initializing ||
                m_AppSettingsDataInitializationStatus == InitializationStatus.Initialized)
            {
                Debug.LogWarning("AppSettings is already initialized and cannot be initialized again.");
                onInitializeFailed?.Invoke();

                return;
            }

            m_AppSettingsDataInitializationStatus = InitializationStatus.Initializing;

            if (dataPersistence != null && dataAdapter != null)
            {
                LoadAppSettingsDataInternal<T>(dataPersistence,
                    (data) =>
                    {
                        InitializeAppSettingsData(data, dataAdapter, onInitializeCompleted, onInitializeFailed);
                    },
                    () =>
                    {
                        InitializeAppSettingsData(null as ISerializableData, dataAdapter, onInitializeCompleted, onInitializeFailed);
                    });
            }
            else
            {
                InitializeAppSettingsData(null as ISerializableData, dataAdapter, onInitializeCompleted, onInitializeFailed);
            }
        }

        static void InitializeAppSettingsData(ISerializableData data, IDataSerializationAdapter dataAdapter, Action onInitializeCompleted = null, Action onInitializeFailed = null)
        {
            bool isInitialized = true;

            try
            {
                GameFrameworkSettings.AppSettingsDatabase.DataAdapter = dataAdapter;
                GameFrameworkSettings.AppSettingsDatabase.InitializeData(data);

                isInitialized = true;
            }
            catch
            {
                isInitialized = false;
            }

            if (isInitialized)
            {
                m_AppSettingsDataInitializationStatus = InitializationStatus.Initialized;
                onInitializeCompleted?.Invoke();
            }
            else
            {
                UninitializeAppSettingsData();

                Debug.LogWarning("AppSettings can't be initialized.");

                m_AppSettingsDataInitializationStatus = InitializationStatus.Failed;
                onInitializeFailed?.Invoke();
            }
        }

        internal static void UninitializeAppSettingsData()
        {
            m_AppSettingsDataInitializationStatus = InitializationStatus.NotInitialized;

            GameFrameworkSettings.AppSettingsDatabase.Unintialize();
        }

        /// <summary>
        /// Asynchronously loads data from the persistence layer and populates managed systems with it
        /// </summary>
        /// <param name="dataPersistence">The persistence layer used to execute the process</param>
        /// <param name="onLoadCompleted">Called when the loading process is completed with success</param>
        /// <param name="onLoadFailed">Called when the loading process failed</param>
        public static void LoadAppSettingsData<T>(
            IDataPersistence dataPersistence,
            Action onLoadCompleted = null,
            Action onLoadFailed = null) where T : ISerializableData
        {
            if (dataPersistence == null)
            {
                onLoadFailed?.Invoke();
                Debug.LogWarning("DataPersistence cannot be null on persistence process.");
                return;
            }

            LoadAppSettingsDataInternal<T>(dataPersistence,
                (data) =>
                {
                    try
                    {
                        FillAppSettingsData(data);
                        onLoadCompleted?.Invoke();
                    }
                    catch
                    {
                        onLoadFailed?.Invoke();
                    }

                }, onLoadFailed);
        }

        private static void LoadAppSettingsDataInternal<T>
        (
            IDataPersistence dataPersistence,
            Action<ISerializableData> onLoadCompleted = null,
            Action onLoadFailed = null) where T : ISerializableData
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
                (e) => { onLoadFailed?.Invoke(); });
        }

        private static void FillAppSettingsData(ISerializableData data)
        {
            GameFrameworkSettings.AppSettingsDatabase.FillFromSerializableData(data);
        }

        /// <summary>
        /// Asynchronously saves data through the persistence layer.
        /// </summary>
        /// <param name="dataPersistence">The persistence layer used to execute the process</param>
        /// <param name="onSaveCompleted">Called when the saving process is completed with success</param>
        /// <param name="onSaveFailed">Called when the saving process failed</param>
        public static void SaveAppSettingsData(
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

            if (!GameFrameworkSettings.AppSettingsDatabase.IsInitialized)
            {
                onSaveFailed?.Invoke();
                Debug.LogWarning("Cannot save AppSettings. AppSettings is not initialized.");
                return;
            }

            var appSettingsData = GameFrameworkSettings.AppSettingsDatabase.GetSerializableData();
            dataPersistence.Save(m_AppSettingsDataPath, appSettingsData, onSaveCompleted, (e) => onSaveFailed.Invoke());
        }
        #endregion

        #region Game Data
        /// <summary>
        /// Initialize the GameFoundation . It need a persistence object to be passed as argument to set the default persistence layer
        /// If the initialization fails, onInitializeFailed will be called with an exception.
        /// </summary>
        /// <param name="dataPersistence">The persistence layer of the Game Foundation. Required and cached for future execution</param>
        /// <param name="onInitializeCompleted">Called when the initialization process is completed with success</param>
        /// <param name="onInitializeFailed">Called when the initialization process failed</param>
        public static void InitializeGameData<T>(
            IDataPersistence dataPersistence = null,
            IDataSerializationAdapter dataAdapter = null,
            Action onInitializeCompleted = null,
            Action onInitializeFailed = null) where T : ISerializableData
        {
            if (m_GameDataInitializationStatus == InitializationStatus.Initializing ||
                m_GameDataInitializationStatus == InitializationStatus.Initialized)
            {
                Debug.LogWarning("Game Data is already initialized and cannot be initialized again.");
                onInitializeFailed?.Invoke();

                return;
            }

            m_GameDataInitializationStatus = InitializationStatus.Initializing;

            if (dataPersistence != null && dataAdapter != null)
            {
                LoadGameDataInternal<T>(dataPersistence,
                    (data) =>
                    {
                        InitializeGameData(data, dataAdapter, onInitializeCompleted, onInitializeFailed);
                    },
                    () =>
                    {
                        InitializeGameData(null as ISerializableData, dataAdapter, onInitializeCompleted, onInitializeFailed);
                    });
            }
            else
            {
                InitializeGameData(null as ISerializableData, dataAdapter, onInitializeCompleted, onInitializeFailed);
            }
        }

        static void InitializeGameData(ISerializableData data, IDataSerializationAdapter dataAdapter, Action onInitializeCompleted = null, Action onInitializeFailed = null)
        {
            bool isInitialized = true;

            try
            {
                GameFrameworkSettings.GameDatabase.DataAdapter = dataAdapter;
                GameFrameworkSettings.GameDatabase.InitializeData(data);

                isInitialized = true;
            }
            catch
            {
                isInitialized = false;
            }

            if (isInitialized)
            {
                m_GameDataInitializationStatus = InitializationStatus.Initialized;
                onInitializeCompleted?.Invoke();
            }
            else
            {
                UninitializeGameData();

                Debug.LogWarning("GameData can't be initialized.");

                m_GameDataInitializationStatus = InitializationStatus.Failed;
                onInitializeFailed?.Invoke();
            }
        }

        internal static void UninitializeGameData()
        {
            m_GameDataInitializationStatus = InitializationStatus.NotInitialized;

            GameFrameworkSettings.GameDatabase.Unintialize();
        }

        /// <summary>
        /// Asynchronously loads data from the persistence layer and populates managed systems with it
        /// </summary>
        /// <param name="dataPersistence">The persistence layer used to execute the process</param>
        /// <param name="onLoadCompleted">Called when the loading process is completed with success</param>
        /// <param name="onLoadFailed">Called when the loading process failed</param>
        public static void LoadGameData<T>(
            IDataPersistence dataPersistence,
            Action onLoadCompleted = null,
            Action onLoadFailed = null) where T : ISerializableData
        {
            if (dataPersistence == null)
            {
                onLoadFailed?.Invoke();
                Debug.LogWarning("DataPersistence cannot be null on persistence process.");
                return;
            }

            LoadGameDataInternal<T>(dataPersistence,
                (data) =>
                {
                    try
                    {
                        FillGameData(data);
                        onLoadCompleted?.Invoke();
                    }
                    catch
                    {
                        onLoadFailed?.Invoke();
                    }

                }, onLoadFailed);
        }

        private static void LoadGameDataInternal<T>
        (
            IDataPersistence dataPersistence,
            Action<ISerializableData> onLoadCompleted = null,
            Action onLoadFailed = null) where T : ISerializableData
        {
            if (dataPersistence == null)
            {
                onLoadFailed?.Invoke();
                Debug.LogWarning("DataPersistence cannot be null on persistence process.");
            }

            dataPersistence.Load<T>(m_GameDataPath,
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
                (e) => { onLoadFailed?.Invoke(); });
        }

        private static void FillGameData(ISerializableData data)
        {
            GameFrameworkSettings.GameDatabase.FillFromSerializableData(data);
        }

        /// <summary>
        /// Asynchronously saves data through the persistence layer.
        /// </summary>
        /// <param name="dataPersistence">The persistence layer used to execute the process</param>
        /// <param name="onSaveCompleted">Called when the saving process is completed with success</param>
        /// <param name="onSaveFailed">Called when the saving process failed</param>
        public static void SaveGameData(
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

            if (!GameFrameworkSettings.GameDatabase.IsInitialized)
            {
                onSaveFailed?.Invoke();
                Debug.LogWarning("Cannot save GameData. GameData is not initialized.");
                return;
            }

            var gameData = GameFrameworkSettings.GameDatabase.GetSerializableData();
            dataPersistence.Save(m_GameDataPath, gameData, onSaveCompleted, (e) => onSaveFailed.Invoke());
        }
        #endregion

        #region Player Data
        /// <summary>
        /// Initialize the GameFoundation . It need a persistence object to be passed as argument to set the default persistence layer
        /// If the initialization fails, onInitializeFailed will be called with an exception.
        /// </summary>
        /// <param name="dataPersistence">The persistence layer of the Game Foundation. Required and cached for future execution</param>
        /// <param name="onInitializeCompleted">Called when the initialization process is completed with success</param>
        /// <param name="onInitializeFailed">Called when the initialization process failed</param>
        public static void InitializePlayerData<T>(
            IDataPersistence dataPersistence = null,
            IDataSerializationAdapter dataAdapter = null,
            Action onInitializeCompleted = null,
            Action onInitializeFailed = null) where T : ISerializableData
        {
            if (m_PlayerDataInitializationStatus == InitializationStatus.Initializing ||
                m_PlayerDataInitializationStatus == InitializationStatus.Initialized)
            {
                Debug.LogWarning("Player Data is already initialized and cannot be initialized again.");
                onInitializeFailed?.Invoke();

                return;
            }

            m_PlayerDataInitializationStatus = InitializationStatus.Initializing;

            if (dataPersistence != null && dataAdapter != null)
            {
                LoadPlayerDataInternal<T>(dataPersistence,
                    (data) =>
                    {
                        InitializePlayerData(data, dataAdapter, onInitializeCompleted, onInitializeFailed);
                    },
                    () =>
                    {
                        InitializePlayerData(null as ISerializableData, dataAdapter, onInitializeCompleted, onInitializeFailed);
                    });
            }
            else
            {
                InitializePlayerData(null as ISerializableData, dataAdapter, onInitializeCompleted, onInitializeFailed);
            }
        }

        static void InitializePlayerData(ISerializableData data, IDataSerializationAdapter dataAdapter, Action onInitializeCompleted = null, Action onInitializeFailed = null)
        {
            bool isInitialized = true;

            try
            {
                GameFrameworkSettings.PlayerDatabase.DataAdapter = dataAdapter;
                GameFrameworkSettings.PlayerDatabase.InitializeData(data);

                isInitialized = true;
            }
            catch
            {
                isInitialized = false;
            }

            if (isInitialized)
            {
                m_PlayerDataInitializationStatus = InitializationStatus.Initialized;
                onInitializeCompleted?.Invoke();
            }
            else
            {
                UninitializePlayerData();

                Debug.LogWarning("PlayerData can't be initialized.");

                m_PlayerDataInitializationStatus = InitializationStatus.Failed;
                onInitializeFailed?.Invoke();
            }
        }

        internal static void UninitializePlayerData()
        {
            m_PlayerDataInitializationStatus = InitializationStatus.NotInitialized;

            GameFrameworkSettings.PlayerDatabase.Unintialize();
        }

        /// <summary>
        /// Asynchronously loads data from the persistence layer and populates managed systems with it
        /// </summary>
        /// <param name="dataPersistence">The persistence layer used to execute the process</param>
        /// <param name="onLoadCompleted">Called when the loading process is completed with success</param>
        /// <param name="onLoadFailed">Called when the loading process failed</param>
        public static void LoadPlayerData<T>(
            IDataPersistence dataPersistence,
            Action onLoadCompleted = null,
            Action onLoadFailed = null) where T : ISerializableData
        {
            if (dataPersistence == null)
            {
                onLoadFailed?.Invoke();
                Debug.LogWarning("DataPersistence cannot be null on persistence process.");
                return;
            }

            LoadPlayerDataInternal<T>(dataPersistence,
                (data) =>
                {
                    try
                    {
                        FillPlayerData(data);
                        onLoadCompleted?.Invoke();
                    }
                    catch
                    {
                        onLoadFailed?.Invoke();
                    }

                }, onLoadFailed);
        }

        private static void LoadPlayerDataInternal<T>
        (
            IDataPersistence dataPersistence,
            Action<ISerializableData> onLoadCompleted = null,
            Action onLoadFailed = null) where T : ISerializableData
        {
            if (dataPersistence == null)
            {
                onLoadFailed?.Invoke();
                Debug.LogWarning("DataPersistence cannot be null on persistence process.");
            }

            dataPersistence.Load<T>(m_PlayerDataPath,
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
                (e) => { onLoadFailed?.Invoke(); });
        }

        private static void FillPlayerData(ISerializableData data)
        {
            GameFrameworkSettings.PlayerDatabase.FillFromSerializableData(data);
        }

        /// <summary>
        /// Asynchronously saves data through the persistence layer.
        /// </summary>
        /// <param name="dataPersistence">The persistence layer used to execute the process</param>
        /// <param name="onSaveCompleted">Called when the saving process is completed with success</param>
        /// <param name="onSaveFailed">Called when the saving process failed</param>
        public static void SavePlayerData(
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

            if (!GameFrameworkSettings.PlayerDatabase.IsInitialized)
            {
                onSaveFailed?.Invoke();
                Debug.LogWarning("Cannot save PlayerData. PlayerData is not initialized.");
                return;
            }

            var PlayerData = GameFrameworkSettings.PlayerDatabase.GetSerializableData();
            dataPersistence.Save(m_PlayerDataPath, PlayerData, onSaveCompleted, (e) => onSaveFailed.Invoke());
        }
        #endregion
    }
}