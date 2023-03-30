using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class PlacedMarkSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CellAddNumberEvent>> _cellAddNumberEvent = Idents.Worlds.Events;

        private readonly EcsPoolInject<PlacedNumber> _placedNumbersFilter = default;

        private readonly EcsCustomInject<SceneData> _sceneData = default;

        public void Run(IEcsSystems systems)
        {
            if (_sceneData.Value.NoteMode) return;
            
            foreach (var entity in _cellAddNumberEvent.Value)
            {
                var eventCellEntity = _cellAddNumberEvent.Pools.Inc1.Get(entity).CellEntity;
                
                if (!_placedNumbersFilter.Value.Has(eventCellEntity))
                    _placedNumbersFilter.Value.Add(eventCellEntity);
            }
        }
    }
}