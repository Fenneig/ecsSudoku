using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class MarkMistakeCellsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlacedNumber, Position, Number>, Exc<SolvedCell>> _placedNumbersFilter =
            default;

        private readonly EcsFilterInject<Inc<MistakeCell, Number>> _mistakeCellsFilter = default;
        private readonly EcsFilterInject<Inc<Position>> _positionFilter = default;

        private readonly EcsCustomInject<Configuration> _config = default;

        private int[,] _field;

        public void Init(IEcsSystems systems)
        {
            _field = new int[_config.Value.GridHeight, _config.Value.GridWidth];
            foreach (var entity in _positionFilter.Value)
            {
                ref var position = ref _positionFilter.Pools.Inc1.Get(entity).Value;
                _field[position.Y, position.X] = entity;
            }
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _mistakeCellsFilter.Value)
            {
                _mistakeCellsFilter.Pools.Inc1.Del(entity);
            }

            foreach (var entity in _placedNumbersFilter.Value)
            {
                var position = _placedNumbersFilter.Pools.Inc2.Get(entity).Value;
                var number = _placedNumbersFilter.Pools.Inc3.Get(entity).Value;
                MarkLinkedCellsAsMistakes(position, number);
            }
        }

        private void MarkLinkedCellsAsMistakes(Int2 position, int number)
        {
            for (int y = 0; y < _config.Value.GridHeight; y++)
            {
                if (_mistakeCellsFilter.Pools.Inc2.Get(_field[y, position.X]).Value != number) continue;
                if (_mistakeCellsFilter.Pools.Inc1.Has(_field[y, position.X])) continue;

                _mistakeCellsFilter.Pools.Inc1.Add(_field[y, position.X]);
            }

            for (int x = 0; x < _config.Value.GridWidth; x++)
            {
                if (_mistakeCellsFilter.Pools.Inc2.Get(_field[position.Y, x]).Value != number) continue;
                if (_mistakeCellsFilter.Pools.Inc1.Has(_field[position.Y, x])) continue;

                _mistakeCellsFilter.Pools.Inc1.Add(_field[position.Y, x]);
            }

            var startY = position.Y / _config.Value.AreaSize * _config.Value.AreaSize;
            var startX = position.X / _config.Value.AreaSize * _config.Value.AreaSize;

            for (int y = startY; y < startY + _config.Value.AreaSize; y++)
            {
                for (int x = startX; x < startX + _config.Value.AreaSize; x++)
                {
                    if (_mistakeCellsFilter.Pools.Inc2.Get(_field[y, x]).Value != number) continue;
                    if (_mistakeCellsFilter.Pools.Inc1.Has(_field[y, x])) continue;

                    _mistakeCellsFilter.Pools.Inc1.Add(_field[y, x]);
                }
            }
        }
    }
}