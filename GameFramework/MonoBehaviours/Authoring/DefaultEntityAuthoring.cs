using System.Collections;
using System.Collections.Generic;
using d4160.GameFramework;
using Unity.Entities;
using UnityEngine;

namespace d4160.GameFramework
{
    public class DefaultEntityAuthoring : AuthoringBehaviourBase<EntityData>
    {
        public virtual int Entity => _data.entity;
    }

    [System.Serializable]
    public struct EntityData : IComponentData
    {
        public int entity;
    }
}

