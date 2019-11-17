namespace d4160.GameFramework
{
    using System.Collections.Generic;
    using d4160.Systems.DataPersistence;

    [System.Serializable]
    public class DefaultPostProcessingSettingsSerializableData : SettingsSerializableDataBase, IPlayerPrefsActions
    {
        public bool bloom;
        public bool colorGrading;
        public bool vignette;

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public DefaultPostProcessingSettingsSerializableData() : base()
        {
        }

        public void Load(bool encrypted = false)
        {
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
        }

        public void Save(bool encrypted = false)
        {
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
        }
    }
}