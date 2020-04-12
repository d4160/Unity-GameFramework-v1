using System.Collections;
using System.Collections.Generic;
using d4160.GameFramework;
#if UNITY_ECS
using Unity.Entities;
#endif
using UnityEngine;

namespace d4160.GameFramework
{
    public class EntityCategoryAuthoring : AuthoringBehaviourBase<EntityCategoryData>
    {
        public virtual int Category => _data.category;
    }

    [System.Serializable]
    public struct EntityCategoryData
#if UNITY_ECS
        : IComponentData
#endif
    {
        public int category;
    }
}

