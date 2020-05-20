using System.Numerics;

#if UNITY_ECS
using Unity.Entities;
using Unity.Mathematics;
#endif

namespace d4160.ECS
{
    [System.Serializable]
    public struct Rotation2D
#if UNITY_ECS
        : IComponentData
#endif
    {
        public int direction;
        public float speed;
    }

    [System.Serializable]
    public struct Rotation
#if UNITY_ECS
        : IComponentData
#endif
    {
#if UNITY_ECS
        public float3 direction;
#else
        public Vector3 direction;
#endif
        public float speed;
    }
}