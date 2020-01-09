namespace d4160.GameFramework
{
    using d4160.DataPersistence;

    [System.Serializable]
    public class DefaultPostProcessingSettingsSerializableData : BaseSerializableData, IStorageHelper
    {
        public bool bloom;
        public bool colorGrading;
        public bool vignette;

        public StorageHelperType StorageHelperType { get; set; }

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public DefaultPostProcessingSettingsSerializableData() : base()
        {
        }

        public virtual void Load(bool encrypted = false, System.Action onCompleted = null)
        {
            switch (StorageHelperType)
            {
                case StorageHelperType.PlayerPrefs:
                    if (encrypted)
                    {
                        bloom = PlayerPrefsUtility.GetEncryptedBool(nameof(bloom), bloom);
                        colorGrading = PlayerPrefsUtility.GetEncryptedBool(nameof(colorGrading), colorGrading);
                        vignette = PlayerPrefsUtility.GetEncryptedBool(nameof(vignette), vignette);
                    }
                    else
                    {
                        bloom = PlayerPrefsUtility.GetBool(nameof(bloom), bloom);
                        colorGrading = PlayerPrefsUtility.GetBool(nameof(colorGrading), colorGrading);
                        vignette = PlayerPrefsUtility.GetBool(nameof(vignette), vignette);
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
                        PlayerPrefsUtility.SetEncryptedBool(nameof(bloom), bloom);
                        PlayerPrefsUtility.SetEncryptedBool(nameof(colorGrading), colorGrading);
                        PlayerPrefsUtility.SetEncryptedBool(nameof(vignette), vignette);
                    }
                    else
                    {
                        PlayerPrefsUtility.SetBool(nameof(bloom), bloom);
                        PlayerPrefsUtility.SetBool(nameof(colorGrading), colorGrading);
                        PlayerPrefsUtility.SetBool(nameof(vignette), vignette);
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