#if UNITY_ECS
using Unity.Entities;
#endif

namespace d4160.ECS
{
    public struct TriggerVolume
#if UNITY_ECS
        : IComponentData
#endif
    {
        public int Type;
        public int CurrentFrame;
    }
}
