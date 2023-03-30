using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class RollBackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RollbackEvent>> _rollBackEvent = Idents.Worlds.Events;
        private readonly EcsFilterInject<Inc<CellViewRef>> _cellViewsFilter = default;

        private readonly EcsPoolInject<Number> _numbersPool = default;
        private readonly EcsPoolInject<PlacedNumber> _placedNumbersPool = default;
        private readonly EcsPoolInject<SolvedCell> _solvedCellsPool = default;

        private readonly EcsCustomInject<SceneData> _sceneData = default;
        private readonly EcsCustomInject<Configuration> _config = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var _ in _rollBackEvent.Value)
            {
                if (_sceneData.Value.PlayerMoves.TryPop(out var playerMove))
                {
                    var cellEntity = playerMove.AffectedCell.CellEntity;
                    var number = playerMove.AffectedCell.Number;

                    _numbersPool.Value.Get(cellEntity).Value = number;
                    _solvedCellsPool.Value.Del(cellEntity);

                    foreach (var affectedNote in playerMove.AffectedNotes)
                    {
                        _cellViewsFilter.Pools.Inc1.Get(affectedNote.CellEntity).Value.Notes
                            .NoteNumberGO[affectedNote.NoteNumber].SetActive(true);
                    }
                    
                    if (number == 0)
                    {
                        _cellViewsFilter.Pools.Inc1.Get(cellEntity).Value.Notes.NoteGO.SetActive(true);
                        _placedNumbersPool.Value.Del(cellEntity);

                        var noteNumber = _cellViewsFilter.Pools.Inc1.Get(cellEntity).Value.Notes.NoteNumberGO;

                        for (int i = 0; i < _config.Value.GridSize; i++)
                        {
                            noteNumber[i].SetActive(false);
                        }

                        foreach (var affectedNote in playerMove.AffectedNotes)
                        {
                            noteNumber[affectedNote.NoteNumber].SetActive(true);
                        }
                    }
                    else
                    {
                        if (!_placedNumbersPool.Value.Has(cellEntity))
                            _placedNumbersPool.Value.Add(cellEntity);
                    }
                }
            }
        }
    }
}