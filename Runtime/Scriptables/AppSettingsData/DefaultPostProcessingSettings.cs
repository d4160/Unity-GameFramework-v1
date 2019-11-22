namespace d4160.GameFramework
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New DefaultPostProcessing Settings_SO.asset", menuName = "Game Framework/App Settings/Default PostProcessing")]
    public class DefaultPostProcessingSettings : AppSettingsScriptableBase
    {
        [SerializeField] protected bool m_bloom;
        [SerializeField] protected bool m_colorGrading;
        [SerializeField] protected bool m_vignette;

        public override ISerializableData GetSerializableData()
        {
            var data = new DefaultPostProcessingSettingsSerializableData();
            data.bloom = m_bloom;
            data.colorGrading = m_colorGrading;
            data.vignette = m_vignette;

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
            var appStatsData = data as DefaultPostProcessingSettingsSerializableData;
            m_bloom = appStatsData.bloom;
            m_colorGrading = appStatsData.colorGrading;
            m_vignette = appStatsData.vignette;
        }
    }
}