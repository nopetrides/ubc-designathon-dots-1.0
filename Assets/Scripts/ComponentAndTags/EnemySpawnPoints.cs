using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace ComponentAndTags
{
    public struct EnemySpawnPoints : IComponentData
    {
        public NativeArray<float3> Value;
    }
}