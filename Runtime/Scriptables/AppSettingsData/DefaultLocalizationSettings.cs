namespace d4160.GameFramework
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    //[CreateAssetMenu(fileName = "New DefaultLocalization Settings_SO.asset", menuName = "Game Framework/App Settings/Default Localization")]
    public abstract class DefaultLocalizationSettings<T> : ScriptableObjectBase<T> where T : BaseSerializableData
    {
        [SerializeField] protected SystemLanguage m_textLanguage;
        [SerializeField] protected SystemLanguage m_voiceLanguage;

        public virtual SystemLanguage TextLanguage 
        {
            get => m_textLanguage;
            set 
            {
                m_textLanguage = value;
                ApplyTextLanguage();
            }
        }

        public virtual SystemLanguage VoiceLanguage 
        {
            get => m_voiceLanguage;
            set 
            {
                m_voiceLanguage = value;
                ApplyVoiceLanguage();
            }
        }

        public virtual void ApplyTextLanguage(){}
        public virtual void ApplyVoiceLanguage(){}
    }
}