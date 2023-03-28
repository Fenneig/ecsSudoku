using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class PlaceNumberSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CellClickedEvent>> _cellClickedEventFilter = Idents.Worlds.Events;

        private readonly EcsFilterInject<Inc<PlacedNumber, Position, Number, CellViewRef>, Exc<SolvedCell>> _filter = default;

        private readonly EcsPoolInject<SolvedCell> _solvedCellsPool = default;

        private readonly EcsCustomInject<SceneData> _sceneData = default;

        public void Run(IEcsSystems systems)
        {
            if (_sceneData.Value.NoteMode) return;
            
            foreach (var eventEntity in _cellClickedEventFilter.Value)
            {
                ref var eventInfo = ref _cellClickedEventFilter.Pools.Inc1.Get(eventEntity);
                
                _filter.Pools.Inc3.Get(eventInfo.CellEntity).Value = eventInfo.Number;

                if (!_filter.Pools.Inc1.Has(eventInfo.CellEntity))
                    _filter.Pools.Inc1.Add(eventInfo.CellEntity);

                var position = _filter.Pools.Inc2.Get(eventInfo.CellEntity).Value;

                if (_sceneData.Value.SolvedField[position.Y, position.X] == eventInfo.Number)
                    _solvedCellsPool.Value.Add(eventInfo.CellEntity);
                else
                    _sceneData.Value.MistakeWasMade++;
            }
        }
    }
}