using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class AnalyzePlacedNumberSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CellClickedEvent>> _cellClickedEventFilter = Idents.Worlds.Events;

        private readonly EcsFilterInject<Inc<PlacedNumber, Position, Number>, Exc<SolvedCell>> _filter = default;

        private readonly EcsPoolInject<SolvedCell> _solvedCellsPool = default;

        private readonly EcsCustomInject<SceneData> _sceneData = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var number = _filter.Pools.Inc3.Get(entity).Value;
                var position = _filter.Pools.Inc2.Get(entity).Value;

                if (_sceneData.Value.SolvedField[position.Y, position.X] == number)
                    _solvedCellsPool.Value.Add(entity);
            }

            foreach (var entity in _cellClickedEventFilter.Value)
            {
                var position = _cellClickedEventFilter.Pools.Inc1.Get(entity).Position;
                var number = _cellClickedEventFilter.Pools.Inc1.Get(entity).Number;

                if (_sceneData.Value.SolvedField[position.Y, position.X] != number)
                    _sceneData.Value.MistakeWasMade++;
            }
        }
    }
}