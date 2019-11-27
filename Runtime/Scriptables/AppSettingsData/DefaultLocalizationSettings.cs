namespace d4160.GameFramework
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New DefaultLocalization Settings_SO.asset", menuName = "Game Framework/App Settings/Default Localization")]
    public class DefaultLocalizationSettings : ScriptableObjectBase
    {
        [SerializeField] protected SystemLanguage m_textLanguage;
        [SerializeField] protected SystemLanguage m_voiceLanguage;

        public override ISerializableData GetSerializableData()
        {
            var data = new DefaultLocalizationSettingsSerializableData();
            data.textLanguage = m_textLanguage;
            data.voiceLanguage = m_voiceLanguage;

            return data;
        }

        public override void InitializeData(ISerializableData data)
        {
            if (data != null)
            {
                FillFromSerializableData(data);
            }
        }

        public override void FillFromSerializableData(ISerializableData data)
        {
            var appStatsData = data as DefaultLocalizationSettingsSerializableData;
            m_textLanguage = appStatsData.textLanguage;
            m_voiceLanguage = appStatsData.voiceLanguage;
        }
    }
}