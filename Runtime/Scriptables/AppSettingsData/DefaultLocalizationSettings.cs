namespace d4160.GameFramework
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New DefaultLocalization Settings_SO.asset", menuName = "Game Framework/App Settings/Default Localization")]
    public class DefaultLocalizationSettings : ScriptableObjectBase<DefaultLocalizationSettingsSerializableData>
    {
        [SerializeField] protected SystemLanguage m_textLanguage;
        [SerializeField] protected SystemLanguage m_voiceLanguage;

        protected override DefaultLocalizationSettingsSerializableData GetSerializableDataGeneric()
        {
            var data = new DefaultLocalizationSettingsSerializableData();
            data.textLanguage = m_textLanguage;
            data.voiceLanguage = m_voiceLanguage;

            return data;
        }

        protected override void FillFromSerializableData(DefaultLocalizationSettingsSerializableData data)
        {
            m_textLanguage = data.textLanguage;
            m_voiceLanguage = data.voiceLanguage;
        }
    }
}