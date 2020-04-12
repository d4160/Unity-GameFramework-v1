#if UNITY_ECS
using Unity.Entities;
using Unity.Mathematics;
#endif

namespace d4160.ECS
{
    [System.Serializable]
    public struct Movement2D
#if UNITY_ECS
        : IComponentData
#endif
    {
#if UNITY_ECS
        public int2 direction;
#endif
        public float speed;
    }

    [System.Serializable]
    public struct Movement
#if UNITY_ECS
        : IComponentData
#endif
    {
#if UNITY_ECS
        public int3 direction;
#endif
        public float speed;
    }
}