using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class CheckForMistakeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CellClickedEvent>> _cellClickedEventFilter = Idents.Worlds.Events;

        private readonly EcsFilterInject<Inc<Position, Number>, Exc<SolvedCell>> _filter = default;

        private readonly EcsCustomInject<SceneData> _sceneData = default;
        
        private readonly EcsPoolInject<SolvedCell> _solvedCellsPool = default;


        public void Run(IEcsSystems systems)
        {
            if (_sceneData.Value.NoteMode) return;
            
            foreach (var eventEntity in _cellClickedEventFilter.Value)
            {
                var eventInfo = _cellClickedEventFilter.Pools.Inc1.Get(eventEntity);
                
                var position = _filter.Pools.Inc1.Get(eventInfo.CellEntity).Value;

                if (_sceneData.Value.SolvedField[position.Y, position.X] == eventInfo.Number)
                    _solvedCellsPool.Value.Add(eventInfo.CellEntity);
                else
                    _sceneData.Value.MistakeWasMade++;
            }
        }
    }
}