namespace d4160.GameFramework
{
    using System.Collections.Generic;
    using d4160.Systems.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    /// <summary>
    /// Don't use JsonUtility to serialize this class since there is an array with abstract type,
    /// which JsonUtility can't support -> https://answers.unity.com/questions/1301570/doesnt-jsonutility-support-arrays-with-abstract-ty.html
    /// </summary>
    [System.Serializable]
    public class AppSettingsSerializableData : ISerializableData
    {
        [SerializeField] protected SettingsSerializableDataBase[] m_settingsData;

        public SettingsSerializableDataBase[] SettingsData { get => m_settingsData; set => m_settingsData = value; }

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public AppSettingsSerializableData()
        {
        }
    }

    /// <summary>
    /// Inherited from this class to allow another lists
    /// </summary>
    [System.Serializable]
    public class ConcreteAppSettingsSerializableData : ISerializableData, IStorageHelper
    {
        [SerializeField] protected DefaultAppStatsSettingsSerializableData m_defaultAppStatsSettingsData;
        [SerializeField] protected DefaultAudioSettingsSerializableData m_defaultAudioSettingsData;
        [SerializeField] protected DefaultGraphicsSettingsSerializableData m_defaultGraphicsSettingsData;
        [SerializeField] protected DefaultLocalizationSettingsSerializableData m_defaultLocalizationSettingsData;
        [SerializeField] protected DefaultPostProcessingSettingsSerializableData m_defaultPostProcessingSettingsData;

        public DefaultAppStatsSettingsSerializableData DefaultAppStatsSettingsData { get => m_defaultAppStatsSettingsData; set => m_defaultAppStatsSettingsData = value; }
        public DefaultAudioSettingsSerializableData DefaultAudioSettingsData { get => m_defaultAudioSettingsData; set => m_defaultAudioSettingsData = value; }
        public DefaultGraphicsSettingsSerializableData DefaultGraphicsSettingsData { get => m_defaultGraphicsSettingsData; set => m_defaultGraphicsSettingsData = value; }
        public DefaultLocalizationSettingsSerializableData DefaultLocalizationSettingsData { get => m_defaultLocalizationSettingsData; set => m_defaultLocalizationSettingsData = value; }
        public DefaultPostProcessingSettingsSerializableData DefaultPostProcessingSettingsData { get => m_defaultPostProcessingSettingsData; set => m_defaultPostProcessingSettingsData = value; }

        protected StorageHelperType m_storageHelperType;
        protected int m_completedCount;

        public StorageHelperType StorageHelperType
        {
            get => m_storageHelperType;
            set
            {
                m_storageHelperType = value;
                m_defaultAppStatsSettingsData.StorageHelperType = value;
                m_defaultAudioSettingsData.StorageHelperType = value;
                m_defaultGraphicsSettingsData.StorageHelperType = value;
                m_defaultLocalizationSettingsData.StorageHelperType = value;
                m_defaultPostProcessingSettingsData.StorageHelperType = value;
            }
        }

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public ConcreteAppSettingsSerializableData()
        {

        }

        public void Save(bool encrypted = false, System.Action onCompleted = null)
        {
            m_completedCount = 0;

            m_defaultAppStatsSettingsData.Save(encrypted, () => OnCompleted(onCompleted));
            m_defaultAudioSettingsData.Save(encrypted, () => OnCompleted(onCompleted));
            m_defaultGraphicsSettingsData.Save(encrypted, () => OnCompleted(onCompleted));
            m_defaultLocalizationSettingsData.Save(encrypted, () => OnCompleted(onCompleted));
            m_defaultPostProcessingSettingsData.Save(encrypted, () => OnCompleted(onCompleted));
        }

        protected virtual void OnCompleted(System.Action onCompleted)
        {
            m_completedCount++;

            if (m_completedCount >= 5)
            {
                onCompleted?.Invoke();
            }
        }

        public void Load(bool encrypted = false, System.Action onCompleted = null)
        {
            m_completedCount = 0;

            m_defaultAppStatsSettingsData.Load(encrypted, () => OnCompleted(onCompleted));
            m_defaultAudioSettingsData.Load(encrypted, () => OnCompleted(onCompleted));
            m_defaultGraphicsSettingsData.Load(encrypted, () => OnCompleted(onCompleted));
            m_defaultLocalizationSettingsData.Load(encrypted, () => OnCompleted(onCompleted));
            m_defaultPostProcessingSettingsData.Load(encrypted, () => OnCompleted(onCompleted));
        }
    }
}