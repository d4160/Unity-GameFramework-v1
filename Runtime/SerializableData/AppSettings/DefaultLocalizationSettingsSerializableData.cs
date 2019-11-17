namespace d4160.GameFramework
{
  using System.Collections.Generic;
  using d4160.Systems.DataPersistence;
    using UnityEngine;

    [System.Serializable]
    public class DefaultLocalizationSettingsSerializableData : SettingsSerializableDataBase, IPlayerPrefsActions
    {
        public SystemLanguage textLanguage;
        public SystemLanguage voiceLanguage;

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public DefaultLocalizationSettingsSerializableData() : base()
        {
        }

        public void Load(bool encrypted = false)
        {
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
        }

        public void Save(bool encrypted = false)
        {
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
        }
    }
}