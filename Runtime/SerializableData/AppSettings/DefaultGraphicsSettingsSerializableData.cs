namespace d4160.GameFramework
{
    using System.Collections.Generic;
    using d4160.Systems.DataPersistence;
  using PlayFab;
  using PlayFab.ClientModels;
  using UnityEngine;

    [System.Serializable]
    public class DefaultGraphicsSettingsSerializableData : SettingsSerializableDataBase, IStorageHelper
    {
        public int resolution;
        public FullScreenMode fullScreenMode;
        public int qualityLevel;
        public int vSyncCount;

        public StorageHelperType StorageHelperType { get; set; }

        public DefaultGraphicsSettingsSerializableData() : base()
        {

        }

        public void Load(bool encrypted = false, System.Action onCompleted = null)
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
                    PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
                        Keys = null
                    }, result => {
                        if (result.Data != null && result.Data.ContainsKey(nameof(resolution)))
                        {
                            resolution = int.Parse(result.Data[nameof(resolution)].Value);
                            fullScreenMode = (FullScreenMode)System.Enum.Parse(typeof(FullScreenMode), result.Data[nameof(fullScreenMode)].Value);
                            qualityLevel = int.Parse(result.Data[nameof(qualityLevel)].Value);
                            vSyncCount = int.Parse(result.Data[nameof(vSyncCount)].Value);
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
                    PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                        Data = new Dictionary<string, string>() {
                            { nameof(resolution), resolution.ToString() },
                            { nameof(fullScreenMode), fullScreenMode.ToString() },
                            { nameof(qualityLevel), qualityLevel.ToString() },
                            { nameof(vSyncCount), vSyncCount.ToString() }
                        }
                    }, (result) => onCompleted?.Invoke(), null);
                break;
            }
        }
    }
}