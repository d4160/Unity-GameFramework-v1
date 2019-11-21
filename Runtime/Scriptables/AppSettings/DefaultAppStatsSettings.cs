namespace d4160.GameFramework
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New DefaultAppStats Settings_SO.asset", menuName = "Game Framework/App Settings/Default AppStats")]
    public class DefaultAppStatsSettings : AppSettingsScriptableBase
    {
        [SerializeField] protected bool m_fps;
        [SerializeField] protected bool m_ram;
        [SerializeField] protected bool m_audio;
        [SerializeField] protected bool m_advancedInfo;

        public override ISerializableData GetSerializableData()
        {
            var data = new DefaultAppStatsSettingsSerializableData();
            data.fps = m_fps;
            data.ram = m_ram;
            data.audio = m_audio;
            data.advancedInfo = m_advancedInfo;

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
            var appStatsData = data as DefaultAppStatsSettingsSerializableData;
            m_fps = appStatsData.fps;
            m_ram = appStatsData.ram;
            m_audio = appStatsData.audio;
            m_advancedInfo = appStatsData.advancedInfo;
        }
    }
}