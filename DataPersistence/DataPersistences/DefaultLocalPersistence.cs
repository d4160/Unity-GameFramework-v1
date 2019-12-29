namespace d4160.Systems.DataPersistence
{
    using System.IO;
    using System;
    using System.Text;
    using UnityEngine.GameFoundation.DataPersistence;

    /// <summary>
    /// Modified version of UnityEngine.GameFoundation.DataPersistence.LocalPersistence.cs
    /// to allow use a full path instead of an identifier only, so we can choose the location folder, for example
    /// also allow us to choose if we use encryption
    /// </summary>
    public class DefaultLocalPersistence : BaseDataPersistence
    {
        protected bool m_encrypted = false;
        protected bool m_saveToPlayerPrefs = false;

        /// <inheritdoc />
        public DefaultLocalPersistence(IDataSerializer serializer, bool encrypted, bool saveToPlayerPrefs = false) : base(serializer)
        {
            m_encrypted = encrypted;
            m_saveToPlayerPrefs = saveToPlayerPrefs;
        }

        /// <inheritdoc />
        public override void Save(string identifier, ISerializableData content, Action onSaveCompleted = null, Action onSaveFailed = null)
        {
            SaveFile(identifier, content, onSaveCompleted, onSaveFailed);
        }

        //We need to extract that code from the Save() because it will be used in the child but the child need to override the Save method sometimes
        //So to not rewrite the same code I have done a function with it
        protected void SaveFile(string identifierFullPath, ISerializableData content, Action onSaveFileCompleted, Action onSaveFileFailed)
        {
            try
            {
                if (m_saveToPlayerPrefs)
                {
                    WriteFile(identifierFullPath, content);
                }
                else
                {
                    string pathBackup = $"{identifierFullPath}_backup";
                    WriteFile(pathBackup, content);
                    File.Copy(pathBackup, identifierFullPath, true);
                }
            }
            catch
            {
                onSaveFileFailed?.Invoke();
                return;
            }

            onSaveFileCompleted?.Invoke();
        }

        /// <inheritdoc />
        public override void Load<T>(string identifierFullPath, Action<ISerializableData> onLoadCompleted = null, Action onLoadFailed = null)
        {
            string path;

            if (m_saveToPlayerPrefs)
            {
                path = identifierFullPath;
            }
            else
            {
                string pathBackup = $"{identifierFullPath}_backup";

                //If the main file doesn't exist we check for backup
                if (System.IO.File.Exists(identifierFullPath))
                {
                    path = identifierFullPath;
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

        private void WriteFile(string path, ISerializableData content)
        {
            if(m_saveToPlayerPrefs)
            {
                var data = SerializeString(content);
                if(m_encrypted)
                    PlayerPrefsUtility.SetEncryptedString(path, data);
                else
                    UnityEngine.PlayerPrefs.SetString(path, data);
            }
            else
            {
                using (var sw = new StreamWriter(path, false, Encoding.Default))
                {
                    var data = SerializeString(content);
                    sw.Write(data);
                }
            }
        }

        protected string ReadFile(string path)
        {
            var str = "";

            if(m_saveToPlayerPrefs)
            {
                if(m_encrypted)
                    str = PlayerPrefsUtility.GetEncryptedString(path);
                else
                    str = UnityEngine.PlayerPrefs.GetString(path);
            }
            else
            {
                FileInfo fileInfo = new FileInfo(path);

                using (StreamReader sr = new StreamReader(fileInfo.OpenRead(), Encoding.Default))
                {
                    str = sr.ReadToEnd();
                }
            }

            return str;
        }

        private static bool DeleteFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("DeleteFile: delete failed." + e);
            }

            return false;
        }

        private string SerializeString(object o)
        {
            return serializer.Serialize(o, m_encrypted);
        }

        protected T DeserializeString<T>(string value) where T : ISerializableData
        {
            return serializer.Deserialize<T>(value, m_encrypted);
        }
    }
}