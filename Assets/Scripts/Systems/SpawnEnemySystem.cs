using ComponentAndTags;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    [BurstCompile]
    public partial struct SpawnEnemySystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            // queue command and spawn on next frame
            // use singleton ecb
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            
            new SpawnEnemyJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Run(); // runs immediate on main thread
            
        }
    }

    [BurstCompile]
    public partial struct SpawnEnemyJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;
        
        private void Execute(ref EnemySpawnTimer timer, ref EntityManagerProperties emProp)
        {
            timer.Value -= DeltaTime;

            if (timer.Value > 0f) return;
            
            timer.Value = emProp.EnemySpawnRate;
            
           var newEnemy = ECB.Instantiate(emProp.EnemyPrefab);
        }
    }
}