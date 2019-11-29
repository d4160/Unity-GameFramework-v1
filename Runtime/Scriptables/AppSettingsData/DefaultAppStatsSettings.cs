namespace d4160.GameFramework
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    //[CreateAssetMenu(fileName = "New DefaultAppStats Settings_SO.asset", menuName = "Game Framework/App Settings/Default AppStats")]
    public abstract class DefaultAppStatsSettings<T> : ScriptableObjectBase<T> where T : BaseSerializableData
    {
        [SerializeField] protected bool m_fps;
        [SerializeField] protected bool m_ram;
        [SerializeField] protected bool m_audio;
        [SerializeField] protected bool m_advancedInfo;
    }
}