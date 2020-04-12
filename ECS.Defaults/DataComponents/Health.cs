#if UNITY_ECS
using Unity.Entities;
#endif

namespace d4160.ECS
{
    [System.Serializable]
    public struct Health
#if UNITY_ECS
        : IComponentData
#endif
    {
        public int healthValue;
        public bool isInvulnerable;
    }
}