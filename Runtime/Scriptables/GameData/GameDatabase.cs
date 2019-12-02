namespace d4160.GameFramework
{
    using d4160.Core;
    using d4160.Systems.DataPersistence;
    using Malee;
#if UNITY_EDITOR
    using NaughtyAttributes;
    using System;
    using System.Reflection;
#endif
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "GameFrameworkDatabase.asset", menuName = "Game Framework/Game Database")]
    public class GameDatabase : ScriptableObject, IDataSerializationActions
    {
        protected IDataSerializationAdapter m_dataAdapter;

        [Reorderable(paginate = true, pageSize = 10)]
        [SerializeField] protected ScriptableObjectReorderableArray m_gameData;

        public ScriptableObjectReorderableArray GameData => m_gameData;

        public IDataSerializationAdapter DataAdapter
        {
            get {
                if (m_dataAdapter == null)
                    m_dataAdapter = new DefaultGameDataSerializationAdapter();

                return m_dataAdapter;
            }
            set => m_dataAdapter = value;
        }

        public ScriptableObjectBase this[int index]
        {
            get
            {
                if (m_gameData.IsValidIndex(index))
                    return m_gameData[index];

                return null;
            }
        }

        public T GetGameData<T>(int index) where T : ScriptableObjectBase
        {
            var gameData = this[index];
            if (gameData)
                return gameData as T;

            return null;
        }

        public string[] GetGameDataNames(int index)
        {
            var curr = this[index];
            if (curr is IArchetypeNames)
                return (curr as IArchetypeNames).ArchetypeNames;

            return null;
        }

        public string[] GetSceneNames(int dataIndex, int catIndex)
        {
            var curr = this[dataIndex];
            if (curr is ISceneNamesGetter)
                return (curr as ISceneNamesGetter).GetSceneNames(catIndex);

            return null;
        }

        public CategoryAndScene[] GetCategorizedScenes(int index)
        {
            var curr = this[index];
            if (curr is ISceneNamesGetter)
                return (curr as ISceneNamesGetter).GetCategorizedScenes();

            return null;
        }

        public ISerializableData GetSerializableData()
        {
            return DataAdapter.GetSerializableData();
        }

        public void FillFromSerializableData(ISerializableData data)
        {
            DataAdapter.FillFromSerializableData(data);
        }

        public void InitializeData(ISerializableData data)
        {
            if(data != null)
                FillFromSerializableData(data);
        }

        public void Unintialize()
        {
            m_dataAdapter = null;
        }

        public bool IsInitialized => m_gameData != null && m_gameData.Length > 0;
    }
}