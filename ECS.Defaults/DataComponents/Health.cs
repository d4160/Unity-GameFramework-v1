using Unity.Entities;

namespace d4160.ECS
{
    [System.Serializable]
    public struct Health : IComponentData
    {
        public int healthValue;
        public bool isInvulnerable;
    }
}