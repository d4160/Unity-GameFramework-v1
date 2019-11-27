namespace d4160.GameFramework
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New DefaultAppStats Settings_SO.asset", menuName = "Game Framework/App Settings/Default AppStats")]
    public class DefaultAppStatsSettings : ScriptableObjectBase<DefaultAppStatsSettingsSerializableData>
    {
        [SerializeField] protected bool m_fps;
        [SerializeField] protected bool m_ram;
        [SerializeField] protected bool m_audio;
        [SerializeField] protected bool m_advancedInfo;

        protected override DefaultAppStatsSettingsSerializableData GetSerializableDataGeneric()
        {
            var data = new DefaultAppStatsSettingsSerializableData();
            data.fps = m_fps;
            data.ram = m_ram;
            data.audio = m_audio;
            data.advancedInfo = m_advancedInfo;

            return data;
        }

        protected override void FillFromSerializableData(DefaultAppStatsSettingsSerializableData data)
        {
            m_fps = data.fps;
            m_ram = data.ram;
            m_audio = data.audio;
            m_advancedInfo = data.advancedInfo;
        }
    }
}