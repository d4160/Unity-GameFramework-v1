namespace d4160.GameFramework
{
  using System.Collections.Generic;
  using d4160.Systems.DataPersistence;
  using PlayFab;
  using PlayFab.ClientModels;
  using UnityEngine;

    [System.Serializable]
    public class DefaultLocalizationSettingsSerializableData : SettingsSerializableDataBase, IStorageHelper
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

        public void Load(bool encrypted = false, System.Action onCompleted = null)
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
                    PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
                        Keys = null
                    }, result => {
                        if (result.Data != null && result.Data.ContainsKey(nameof(textLanguage)))
                        {
                            textLanguage = (SystemLanguage)System.Enum.Parse(typeof(SystemLanguage),result.Data[nameof(textLanguage)].Value);
                            voiceLanguage = (SystemLanguage)System.Enum.Parse(typeof(SystemLanguage), result.Data[nameof(voiceLanguage)].Value);
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
                    PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                        Data = new Dictionary<string, string>() {
                            { nameof(textLanguage), textLanguage.ToString() },
                            { nameof(voiceLanguage), voiceLanguage.ToString() },
                        }
                    }, (result) => onCompleted?.Invoke(), null);
                break;
            }
        }
    }
}