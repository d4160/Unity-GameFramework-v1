namespace d4160.GameFramework
{
    using d4160.DataPersistence;
    using UnityEngine;

    [System.Serializable]
    public class DefaultGraphicsSettingsSerializableData : BaseSerializableData, IStorageHelper
    {
        public int resolution;
        public FullScreenMode fullScreenMode;
        public int qualityLevel;
        public int vSyncCount;

        public StorageHelperType StorageHelperType { get; set; }

        public DefaultGraphicsSettingsSerializableData() : base()
        {

        }

        public virtual void Load(bool encrypted = false, System.Action onCompleted = null)
        {
            switch (StorageHelperType)
            {
                case StorageHelperType.PlayerPrefs:
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

                    onCompleted?.Invoke();
                break;
                case StorageHelperType.PlayFab:
                    LoadForPlayFab(encrypted, onCompleted);
                break;
            }
        }

        protected virtual void LoadForPlayFab(bool encrypted = false, System.Action onCompleted = null)
        {
        }

        public virtual void Save(bool encrypted = false, System.Action onCompleted = null)
        {
            switch (StorageHelperType)
            {
                case StorageHelperType.PlayerPrefs:
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

                    onCompleted?.Invoke();
                break;

                case StorageHelperType.PlayFab:
                    SaveForPlayFab(encrypted, onCompleted);
                break;
            }
        }

        protected virtual void SaveForPlayFab(bool encrypted = false, System.Action onCompleted = null)
        {
        }
    }
}