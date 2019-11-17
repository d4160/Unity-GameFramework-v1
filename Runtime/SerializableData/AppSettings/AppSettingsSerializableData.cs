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
    public class ConcreteAppSettingsSerializableData : ISerializableData, IPlayerPrefsActions
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

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public ConcreteAppSettingsSerializableData()
        {

        }

        public void Save(bool encrypted = false)
        {
            m_defaultAppStatsSettingsData.Save(encrypted);
            m_defaultAudioSettingsData.Save(encrypted);
            m_defaultGraphicsSettingsData.Save(encrypted);
            m_defaultLocalizationSettingsData.Save(encrypted);
            m_defaultPostProcessingSettingsData.Save(encrypted);
        }

        public void Load(bool encrypted = false)
        {
            m_defaultAppStatsSettingsData.Load(encrypted);
            m_defaultAudioSettingsData.Load(encrypted);
            m_defaultGraphicsSettingsData.Load(encrypted);
            m_defaultLocalizationSettingsData.Load(encrypted);
            m_defaultPostProcessingSettingsData.Load(encrypted);
        }
    }
}