using System.Collections;
using System.Collections.Generic;
using d4160.GameFramework;
#if UNITY_ECS
using Unity.Entities;
#endif
using UnityEngine;

namespace d4160.GameFramework
{
    public class DefaultEntityAuthoring : AuthoringBehaviourBase<EntityData>

    {
        public virtual int Entity => _data.entity;
    }

    [System.Serializable]
    public struct EntityData
#if UNITY_ECS
        : IComponentData
#endif
    {
        public int entity;
    }
}

