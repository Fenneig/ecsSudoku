using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class InitFieldSystem : IEcsInitSystem
    {
        private readonly EcsPoolInject<Cell> _cellPool = default;
        private readonly EcsPoolInject<Position> _positionPool = default;

        private readonly EcsCustomInject<Configuration> _config = default;

        public void Init(IEcsSystems systems)
        {
            var world = _cellPool.Value.GetWorld();
            
            for (int y = 0; y < _config.Value.GridHeight; y++)
            {
                for (int x = 0; x < _config.Value.GridWidth; x++)
                {
                    var cellEntity = world.NewEntity();
                    _cellPool.Value.Add(cellEntity);
                    _positionPool.Value.Add(cellEntity).Value = new Int2(x, y);
                }
            }
        }
    }
}