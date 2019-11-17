namespace d4160.GameFramework
{
    using System.Collections.Generic;
    using d4160.Systems.DataPersistence;
    using UnityEngine;

    [System.Serializable]
    public class DefaultGraphicsSettingsSerializableData : SettingsSerializableDataBase, IPlayerPrefsActions
    {
        public int resolution;
        public FullScreenMode fullScreenMode;
        public int qualityLevel;
        public int vSyncCount;

        public DefaultGraphicsSettingsSerializableData() : base()
        {

        }

        public void Load(bool encrypted = false)
        {
            if (encrypted)
            {
                resolution = PlayerPrefsUtility.GetEncryptedInt(nameof(resolution), resolution);
                fullScreenMode = (FullScreenMode)PlayerPrefsUtility.GetEncryptedInt(nameof(fullScreenMode), (int)fullScreenMode);
                qualityLevel = PlayerPrefsUtility.GetEncryptedInt(nameof(qualityLevel), qualityLevel);
                vSyncCount = PlayerPrefsUtility.GetEncryptedInt(nameof(vSyncCount), vSyncCount);
            }
            else
            {
                resolution = PlayerPrefs.GetInt(nameof(resolution), resolution);
                fullScreenMode = PlayerPrefsUtility.GetEnum(nameof(fullScreenMode), fullScreenMode);
                qualityLevel = PlayerPrefs.GetInt(nameof(qualityLevel), qualityLevel);
                vSyncCount = PlayerPrefs.GetInt(nameof(vSyncCount), vSyncCount);
            }
        }

        public void Save(bool encrypted = false)
        {
            if (encrypted)
            {
                PlayerPrefsUtility.SetEncryptedInt(nameof(resolution), resolution);
                PlayerPrefsUtility.SetEncryptedInt(nameof(fullScreenMode), (int)fullScreenMode);
                PlayerPrefsUtility.SetEncryptedInt(nameof(qualityLevel), qualityLevel);
                PlayerPrefsUtility.SetEncryptedInt(nameof(vSyncCount), vSyncCount);
            }
            else
            {
                PlayerPrefs.SetInt(nameof(resolution), resolution);
                PlayerPrefsUtility.SetEnum(nameof(fullScreenMode), fullScreenMode);
                PlayerPrefs.SetInt(nameof(qualityLevel), qualityLevel);
                PlayerPrefs.SetInt(nameof(vSyncCount), vSyncCount);
            }
        }
    }
}