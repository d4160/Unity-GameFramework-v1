using System;
using Unity.Entities;

namespace d4160.ECS
{
    [Serializable]
    public struct Limit : IComponentData
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