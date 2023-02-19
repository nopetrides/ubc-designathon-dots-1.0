using AuthoringAndMono;
using ComponentAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnDropPointSystem : ISystem
    {
        private Random _random;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EntityManagerProperties>();
            _random = Random.CreateFromIndex(100);
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // turn off the system so it won't run again
            // TODO don't make all spawns instant
            state.Enabled = false;
            // do once code
            var myManagerEntity = SystemAPI.GetSingletonEntity<EntityManagerProperties>();
            var entityManger = SystemAPI.GetAspectRO<DropAspect>(myManagerEntity);
            
            // loop and spawn all drop points
            // TODO don't use local or temp ecb
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            for (int i = 0; i < entityManger.NumberDropPointsToSpawn; i++)
            {
                var newDropPoint = ecb.Instantiate(entityManger.DropPointPrefab);
                var newDropPointTransform = entityManger.GetRandomDropTransform(ref _random);
                ecb.SetComponent(newDropPoint, LocalTransform.FromPosition(newDropPointTransform.Position));
            }
            ecb.Playback(state.EntityManager);
        }
    }
}