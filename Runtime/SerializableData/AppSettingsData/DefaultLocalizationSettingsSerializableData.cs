namespace d4160.GameFramework
{
    using d4160.Systems.DataPersistence;
    using UnityEngine;

    [System.Serializable]
    public class DefaultLocalizationSettingsSerializableData : BaseSerializableData, IStorageHelper
    {
        public SystemLanguage textLanguage;
        public SystemLanguage voiceLanguage;

        public StorageHelperType StorageHelperType { get; set; }

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public DefaultLocalizationSettingsSerializableData() : base()
        {
        }

        public virtual void Load(bool encrypted = false, System.Action onCompleted = null)
        {
            switch (StorageHelperType)
            {
                case StorageHelperType.PlayerPrefs:
                    if (encrypted)
                    {
                        textLanguage = (SystemLanguage)PlayerPrefsUtility.GetEncryptedInt(nameof(textLanguage), (int)textLanguage);
                        voiceLanguage = (SystemLanguage)PlayerPrefsUtility.GetEncryptedInt(nameof(voiceLanguage), (int)voiceLanguage);
                    }
                    else
                    {
                        textLanguage = PlayerPrefsUtility.GetEnum(nameof(textLanguage), textLanguage);
                        voiceLanguage = PlayerPrefsUtility.GetEnum(nameof(voiceLanguage), voiceLanguage);
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
                        PlayerPrefsUtility.SetEncryptedInt(nameof(textLanguage), (int)textLanguage);
                        PlayerPrefsUtility.SetEncryptedInt(nameof(voiceLanguage), (int)voiceLanguage);
                    }
                    else
                    {
                        PlayerPrefsUtility.SetEnum(nameof(textLanguage), textLanguage);
                        PlayerPrefsUtility.SetEnum(nameof(voiceLanguage), voiceLanguage);
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