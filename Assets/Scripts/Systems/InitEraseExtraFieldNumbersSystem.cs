using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsSudoku.Systems
{
    public class InitEraseExtraFieldNumbersSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<Number, Position>> _numberFilter = default;
        
        private readonly EcsPoolInject<SolvedCell> _solvedCellPool = default;
        
        private readonly EcsCustomInject<SceneData> _sceneData = default;
        private readonly EcsCustomInject<Configuration> _config = default;

        public void Init(IEcsSystems systems)
        {
            int[,] field = new int[_config.Value.GridSize, _config.Value.GridSize];
            
            foreach (var entity in _numberFilter.Value)
            {
                var position = _numberFilter.Pools.Inc2.Get(entity).Value;
                field[position.Y, position.X] = _numberFilter.Pools.Inc1.Get(entity).Value;
            }

            int count = _config.Value.GridSize * _config.Value.GridSize - _sceneData.Value.Difficult;

            while (count != 0)
            {
                int cellId = Random.Range(0, _config.Value.GridSize * _config.Value.GridSize);
                int y = cellId / _config.Value.GridSize;
                int x = cellId % _config.Value.GridSize;
                if (field[y, x] != 0)
                {
                    count--;
                    field[y, x] = 0;
                }
            }

            foreach (var entity in _numberFilter.Value)
            {
                var position = _numberFilter.Pools.Inc2.Get(entity).Value;
                var value = field[position.Y, position.X];
                _numberFilter.Pools.Inc1.Get(entity).Value = value;
                if (value != 0) _solvedCellPool.Value.Add(entity);
            }
        }
    }
}