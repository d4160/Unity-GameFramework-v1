using System.Collections;
using System.Collections.Generic;
using d4160.GameFramework;
using Unity.Entities;
using UnityEngine;

namespace d4160.GameFramework
{
    public class EntityCategoryAuthoring : AuthoringBehaviourBase<EntityCategoryData>
    {
        public virtual int Category => _data.category;
    }

    [System.Serializable]
    public struct EntityCategoryData : IComponentData
    {
        public int category;
    }
}

