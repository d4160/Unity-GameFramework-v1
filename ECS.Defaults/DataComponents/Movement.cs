using Unity.Entities;
using Unity.Mathematics;

namespace d4160.ECS
{
    [System.Serializable]
    public struct Movement2D : IComponentData
    {
        public int2 direction;
        public float speed;
    }

    [System.Serializable]
    public struct Movement : IComponentData
    {
        public int3 direction;
        public float speed;
    }
}