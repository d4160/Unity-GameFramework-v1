namespace d4160.GameFramework
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    //[CreateAssetMenu(fileName = "New DefaultLocalization Settings_SO.asset", menuName = "Game Framework/App Settings/Default Localization")]
    public abstract class DefaultLocalizationSettings<T> : ScriptableObjectBase<T> where T : BaseSerializableData
    {
        [SerializeField] protected SystemLanguage m_textLanguage;
        [SerializeField] protected SystemLanguage m_voiceLanguage;
    }
}