namespace d4160.Systems.DataPersistence
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Enum to determine the save data path for player account data.
    /// </summary>
    public enum SaveDataPath
    {
        /// <summary>
        /// Application.dataPath
        /// </summary>
        DataPath,

        /// <summary>
        /// Application.persistentDataPath
        /// </summary>
        PersistentDataPath,
        PlayerPrefs,

        Desktop
    }

    public static class DataPath
    {
        public static string GetPath(SaveDataPath folderPath)
        {
            switch(folderPath)
            {
                case SaveDataPath.DataPath:
                    return Application.dataPath;
                case SaveDataPath.PersistentDataPath:
                    return Application.persistentDataPath;
                case SaveDataPath.Desktop:
                    return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                default:
                    return Application.dataPath;
            }
        }

        public static string GetPath(SaveDataPath folderPath, string fileName, string extension)
        {
            if (folderPath == SaveDataPath.PlayerPrefs)
            {
                return $"{fileName}.{extension}";
            }
            else
            {
                var fPath = GetPath(folderPath);
                return System.IO.Path.Combine(fPath, $"{fileName}.{extension}");
            }
        }
    }
}