﻿namespace d4160.GameFramework
{
    using d4160.Core;
    using d4160.Systems.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public abstract class DataController<T> : Singleton<T> where T : MonoBehaviour
    {
        [SerializeField] protected DefaultDataLoader m_dataLoader;

        public DefaultDataLoader DataLoader => m_dataLoader;
        public AuthenticatorControllerBase Authenticator => m_dataLoader.Authenticator;

        //public abstract void SetData<T>(T data, int dataIdxOrId = 0) where T : class, ISerializableData;

        public abstract T GetScriptable<T>(int dataIdxOrId = 0) where T : ScriptableObjectBase;
    }
}