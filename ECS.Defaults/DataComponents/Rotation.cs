using Unity.Entities;
using Unity.Mathematics;

namespace d4160.ECS
{
    [System.Serializable]
    public struct Rotation2D : IComponentData
    {
        public int direction;
        public float speed;
    }

    [System.Serializable]
    public struct Rotation : IComponentData
    {
        public int3 direction;
        public float speed;
    }
}