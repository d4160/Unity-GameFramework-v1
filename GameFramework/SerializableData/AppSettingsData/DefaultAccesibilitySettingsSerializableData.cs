namespace d4160.GameFramework
{
    using DataPersistence;
    using UnityEngine;

    [System.Serializable]
    public class DefaultAccesibilitySettingsSerializableData : BaseSerializableData, IStorageHelper
    {
        public float uiScale;
        public bool subtitles;

        public StorageHelperType StorageHelperType { get; set; }

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public DefaultAccesibilitySettingsSerializableData() : base()
        {
        }

        public virtual void Load(bool encrypted = false, System.Action onCompleted = null)
        {
            switch (StorageHelperType)
            {
                case StorageHelperType.PlayerPrefs:
                    if (encrypted)
                    {
                        uiScale = PlayerPrefsUtility.GetEncryptedFloat(nameof(uiScale), uiScale);
                        subtitles = PlayerPrefsUtility.GetEncryptedBool(nameof(subtitles), subtitles);
                    }
                    else
                    {
                        uiScale = PlayerPrefs.GetFloat(nameof(uiScale), uiScale);
                        subtitles = PlayerPrefsUtility.GetBool(nameof(subtitles), subtitles);
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
                        PlayerPrefsUtility.SetEncryptedFloat(nameof(uiScale), uiScale);
                        PlayerPrefsUtility.SetEncryptedBool(nameof(subtitles), subtitles);
                    }
                    else
                    {
                        PlayerPrefs.SetFloat(nameof(uiScale), uiScale);
                        PlayerPrefsUtility.SetBool(nameof(subtitles), subtitles);
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