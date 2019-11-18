namespace d4160.GameFramework
{
    using System.Collections.Generic;
    using d4160.Systems.DataPersistence;
  using PlayFab;
  using PlayFab.ClientModels;

  [System.Serializable]
    public class DefaultPostProcessingSettingsSerializableData : SettingsSerializableDataBase, IStorageHelper
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

        public void Load(bool encrypted = false, System.Action onCompleted = null)
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
                    PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
                        Keys = null
                    }, result => {
                        if (result.Data != null && result.Data.ContainsKey(nameof(bloom)))
                        {
                            bloom = bool.Parse(result.Data[nameof(bloom)].Value);
                            colorGrading = bool.Parse(result.Data[nameof(colorGrading)].Value);
                            vignette = bool.Parse(result.Data[nameof(vignette)].Value);
                        }

                        onCompleted?.Invoke();
                    }, null);
                break;
            }
        }

        public void Save(bool encrypted = false, System.Action onCompleted = null)
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
                    PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                        Data = new Dictionary<string, string>() {
                            { nameof(bloom), bloom.ToString() },
                            { nameof(colorGrading), colorGrading.ToString() },
                            { nameof(vignette), vignette.ToString() },
                        }
                    }, (result) => onCompleted?.Invoke(), null);
                break;
            }
        }
    }
}