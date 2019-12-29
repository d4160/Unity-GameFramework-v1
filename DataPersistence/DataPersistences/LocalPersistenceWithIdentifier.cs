namespace d4160.Systems.DataPersistence
{
    using System;
    using UnityEngine.GameFoundation.DataPersistence;

    public class LocalPersistenceWithIdentifier : DefaultLocalPersistence
    {
        protected string m_identifier;

        public LocalPersistenceWithIdentifier(IDataSerializer serializer, bool encrypted, bool saveToPlayerPrefs = false) : base(serializer, encrypted, saveToPlayerPrefs)
        {
        }

        public virtual void SetIdentifier(string identifier)
        {
            m_identifier = identifier;
        }

        public override void Save(string identifier, ISerializableData content, Action onSaveCompleted = null, Action onSaveFailed = null)
        {
            SaveFile(m_identifier, content, onSaveCompleted, onSaveFailed);
        }

        public override void Load<T>(string identifierFullPath, Action<ISerializableData> onLoadCompleted = null, Action onLoadFailed = null)
        {
            string path;

            if (m_saveToPlayerPrefs)
            {
                path = m_identifier;
            }
            else
            {
                string pathBackup = $"{m_identifier}_backup";

                //If the main file doesn't exist we check for backup
                if (System.IO.File.Exists(m_identifier))
                {
                    path = m_identifier;
                }
                else if (System.IO.File.Exists(pathBackup))
                {
                    path = pathBackup;
                }
                else
                {
                    onLoadFailed?.Invoke();
                    return;
                }
            }

            var strData = "";
            try
            {
                strData = ReadFile(path);
            }
            catch
            {
                onLoadFailed?.Invoke();
                return;
            }

            var data = DeserializeString<T>(strData);
            onLoadCompleted?.Invoke(data);
        }
    }
}