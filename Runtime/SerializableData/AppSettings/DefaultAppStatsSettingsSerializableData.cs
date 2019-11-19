namespace d4160.GameFramework
{
  using System.Collections.Generic;
  using d4160.Systems.DataPersistence;
  #if PLAYFAB
  using PlayFab;
  using PlayFab.ClientModels;
  #endif


  [System.Serializable]
    public class DefaultAppStatsSettingsSerializableData : SettingsSerializableDataBase, IStorageHelper
    {
        public bool fps;
        public bool ram;
        public bool audio;
        public bool advancedInfo;

        public StorageHelperType StorageHelperType { get; set; }

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public DefaultAppStatsSettingsSerializableData() : base()
        {
        }

        public void Load(bool encrypted = false, System.Action onCompleted = null)
        {
            switch (StorageHelperType)
            {
                case StorageHelperType.PlayerPrefs:
                    if (encrypted)
                    {
                        fps = PlayerPrefsUtility.GetEncryptedBool(nameof(fps), fps);
                        ram = PlayerPrefsUtility.GetEncryptedBool(nameof(ram), ram);
                        audio = PlayerPrefsUtility.GetEncryptedBool(nameof(audio), audio);
                        advancedInfo = PlayerPrefsUtility.GetEncryptedBool(nameof(advancedInfo), advancedInfo);
                    }
                    else
                    {
                        fps = PlayerPrefsUtility.GetBool(nameof(fps), fps);
                        ram = PlayerPrefsUtility.GetBool(nameof(ram), ram);
                        audio = PlayerPrefsUtility.GetBool(nameof(audio), audio);
                        advancedInfo = PlayerPrefsUtility.GetBool(nameof(advancedInfo), advancedInfo);
                    }

                    onCompleted?.Invoke();
                break;
                case StorageHelperType.PlayFab:
                #if PLAYFAB
                    PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
                        Keys = null
                    }, result => {
                        if (result.Data != null && result.Data.ContainsKey(nameof(fps)))
                        {
                            fps = bool.Parse(result.Data[nameof(fps)].Value);
                            ram = bool.Parse(result.Data[nameof(ram)].Value);
                            audio = bool.Parse(result.Data[nameof(audio)].Value);
                            advancedInfo = bool.Parse(result.Data[nameof(advancedInfo)].Value);
                        }

                        onCompleted?.Invoke();
                    }, null);
                    #endif
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
                        PlayerPrefsUtility.SetEncryptedBool(nameof(fps), fps);
                        PlayerPrefsUtility.SetEncryptedBool(nameof(ram), ram);
                        PlayerPrefsUtility.SetEncryptedBool(nameof(audio), audio);
                        PlayerPrefsUtility.SetEncryptedBool(nameof(advancedInfo), advancedInfo);
                    }
                    else
                    {
                        PlayerPrefsUtility.SetBool(nameof(fps), fps);
                        PlayerPrefsUtility.SetBool(nameof(ram), ram);
                        PlayerPrefsUtility.SetBool(nameof(audio), audio);
                        PlayerPrefsUtility.SetBool(nameof(advancedInfo), advancedInfo);
                    }

                    onCompleted?.Invoke();
                break;

                case StorageHelperType.PlayFab:
                #if PLAYFAB
                    PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                        Data = new Dictionary<string, string>() {
                            { nameof(fps), fps.ToString() },
                            { nameof(ram), ram.ToString() },
                            { nameof(audio), audio.ToString() },
                            { nameof(advancedInfo), advancedInfo.ToString() }
                        }
                    }, (result) => onCompleted?.Invoke(), null);
                    #endif
                break;
            }
        }
    }
}