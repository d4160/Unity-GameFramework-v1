using UnityEngine;

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
        public float2 direction;
#else
        public Vector2 direction;
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
        public float3 direction;
#else
        public Vector3 direction;
#endif
        public float speed;
    }
}