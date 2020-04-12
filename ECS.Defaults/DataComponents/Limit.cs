using System;
#if UNITY_ECS
using Unity.Entities;
#endif

namespace d4160.ECS
{
    [Serializable]
    public struct Limit
#if UNITY_ECS
        : IComponentData
#endif
    {
        public LimitSide side;
        public float limitValue;
    }

    public enum LimitSide
    {
        Lower,
        Upper
    }
}