namespace d4160.GameFramework
{
    using d4160.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    /// <summary>
    /// Don't use JsonUtility to serialize this class since there is an array with abstract type,
    /// which JsonUtility can't support -> https://answers.unity.com/questions/1301570/doesnt-jsonutility-support-arrays-with-abstract-ty.html
    /// </summary>
    [System.Serializable]
    public class DefaultAppSettingsSerializableData : IGenericSerializableData
    {
        [SerializeField] protected BaseSerializableData[] m_settingsData;

        public BaseSerializableData[] SerializableData { get => m_settingsData; set => m_settingsData = value; }

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public DefaultAppSettingsSerializableData()
        {
        }
    }

    /// <summary>
    /// Inherited from this class to allow another lists
    /// </summary>
    [System.Serializable]
    public class DefaultConcreteAppSettingsSerializableData : ISerializableData, IStorageHelper
    {
        [SerializeField] protected DefaultAppStatsSettingsSerializableData m_appStatsSettingsData;
        [SerializeField] protected DefaultAudioSettingsSerializableData m_audioSettingsData;
        [SerializeField] protected DefaultGraphicsSettingsSerializableData m_graphicsSettingsData;
        [SerializeField] protected DefaultLocalizationSettingsSerializableData m_localizationSettingsData;
        [SerializeField] protected DefaultPostProcessingSettingsSerializableData m_postProcessingSettingsData;

        public DefaultAppStatsSettingsSerializableData AppStatsSettingsData { get => m_appStatsSettingsData; set => m_appStatsSettingsData = value; }
        public DefaultAudioSettingsSerializableData AudioSettingsData { get => m_audioSettingsData; set => m_audioSettingsData = value; }
        public DefaultGraphicsSettingsSerializableData GraphicsSettingsData { get => m_graphicsSettingsData; set => m_graphicsSettingsData = value; }
        public DefaultLocalizationSettingsSerializableData LocalizationSettingsData { get => m_localizationSettingsData; set => m_localizationSettingsData = value; }
        public DefaultPostProcessingSettingsSerializableData PostProcessingSettingsData { get => m_postProcessingSettingsData; set => m_postProcessingSettingsData = value; }

        protected StorageHelperType m_storageHelperType;
        protected int m_completedCount;

        public StorageHelperType StorageHelperType
        {
            get => m_storageHelperType;
            set
            {
                m_storageHelperType = value;
                m_appStatsSettingsData.StorageHelperType = value;
                m_audioSettingsData.StorageHelperType = value;
                m_graphicsSettingsData.StorageHelperType = value;
                m_localizationSettingsData.StorageHelperType = value;
                m_postProcessingSettingsData.StorageHelperType = value;
            }
        }

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public DefaultConcreteAppSettingsSerializableData()
        {

        }

        public virtual void Save(bool encrypted = false, System.Action onCompleted = null)
        {
            m_completedCount = 0;

            m_appStatsSettingsData.Save(encrypted, () => OnCompleted(onCompleted));
            m_audioSettingsData.Save(encrypted, () => OnCompleted(onCompleted));
            m_graphicsSettingsData.Save(encrypted, () => OnCompleted(onCompleted));
            m_localizationSettingsData.Save(encrypted, () => OnCompleted(onCompleted));
            m_postProcessingSettingsData.Save(encrypted, () => OnCompleted(onCompleted));
        }

        protected virtual void OnCompleted(System.Action onCompleted)
        {
            m_completedCount++;

            if (m_completedCount >= 5)
            {
                onCompleted?.Invoke();
            }
        }

        public virtual void Load(bool encrypted = false, System.Action onCompleted = null)
        {
            m_completedCount = 0;

            m_appStatsSettingsData.Load(encrypted, () => OnCompleted(onCompleted));
            m_audioSettingsData.Load(encrypted, () => OnCompleted(onCompleted));
            m_graphicsSettingsData.Load(encrypted, () => OnCompleted(onCompleted));
            m_localizationSettingsData.Load(encrypted, () => OnCompleted(onCompleted));
            m_postProcessingSettingsData.Load(encrypted, () => OnCompleted(onCompleted));
        }
    }
}