using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class ClickAnalyzeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CellViewRef, Position>> _cellViewFilter = default;
        private readonly EcsFilterInject<Inc<LinkedCell>> _linkedCellsFilter = default;
        private readonly EcsFilterInject<Inc<Clicked>> _clickedFilter = default;
        private readonly EcsFilterInject<Inc<CellClickedEvent>> _cellClickedEventFilter = default;
        
        private readonly EcsCustomInject<Configuration> _config = default;
        
        private int[,] _field;

        public void Init(IEcsSystems systems)
        {
            _field = new int[_config.Value.GridHeight, _config.Value.GridWidth];
            foreach (var entity in _cellViewFilter.Value)
            {
                ref var position = ref _cellViewFilter.Pools.Inc2.Get(entity).Value;
                _field[position.Y, position.X] = entity;
            }
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var clickedEventEntity in _cellClickedEventFilter.Value)
            {
                var clickedCellPosition = _cellClickedEventFilter.Pools.Inc1.Get(clickedEventEntity).CellPosition;
                
                foreach (var entity in _linkedCellsFilter.Value)
                {
                    _linkedCellsFilter.Pools.Inc1.Del(entity);
                }

                foreach (var entity in _clickedFilter.Value)
                {
                    _clickedFilter.Pools.Inc1.Del(entity);
                }

                MarkLinkedCells(clickedCellPosition);
            
                _clickedFilter.Pools.Inc1.Add(_field[clickedCellPosition.Y, clickedCellPosition.X]);
            }
        }

        private void MarkLinkedCells(Int2 position)
        {
            for (int y = 0; y < _config.Value.GridHeight; y++)
            {
                if (_linkedCellsFilter.Pools.Inc1.Has(_field[y, position.X])) continue;
                
                _linkedCellsFilter.Pools.Inc1.Add(_field[y, position.X]);
            }

            for (int x = 0; x < _config.Value.GridWidth; x++)
            {
                if (_linkedCellsFilter.Pools.Inc1.Has(_field[position.Y, x])) continue;
                
                _linkedCellsFilter.Pools.Inc1.Add(_field[position.Y, x]);
            }

            var startY = position.Y / _config.Value.AreaSize * _config.Value.AreaSize;
            var startX = position.X / _config.Value.AreaSize * _config.Value.AreaSize;

            for (int y = startY; y < startY + _config.Value.AreaSize; y++)
            {
                for (int x = startX; x < startX + _config.Value.AreaSize; x++)
                {
                    if (_linkedCellsFilter.Pools.Inc1.Has(_field[y, x])) continue;
                    
                    _linkedCellsFilter.Pools.Inc1.Add(_field[y, x]);
                }
            }
        }
    }
}