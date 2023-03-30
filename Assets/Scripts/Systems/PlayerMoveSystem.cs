using System.Collections.Generic;
using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CellEraseEvent>> _eraseEvent = Idents.Worlds.Events;
        private readonly EcsFilterInject<Inc<CellAddNumberEvent>> _addNumberEvent = Idents.Worlds.Events;
        private readonly EcsFilterInject<Inc<LinkedCell, CellViewRef>> _linkedCellsFilter = default;
        private readonly EcsFilterInject<Inc<Number, Position>> _numberFilter = default;

        private readonly EcsPoolInject<CellViewRef> _cellsViewPool = default;

        private readonly EcsCustomInject<SceneData> _sceneData = default;
        private readonly EcsCustomInject<Configuration> _config = default;

        public void Run(IEcsSystems systems)
        {
            AddMove();

            EraseMove();
        }

        private void AddMove()
        {
            if (_sceneData.Value.NoteMode) return;

            foreach (var eventEntity in _addNumberEvent.Value)
            {
                var cellEntity = _addNumberEvent.Pools.Inc1.Get(eventEntity).CellEntity;
                var prevNumber = _numberFilter.Pools.Inc1.Get(cellEntity).Value;
                var noteNumber = _addNumberEvent.Pools.Inc1.Get(eventEntity).Number - 1;

                List<AffectedNote> affectedNotes = new List<AffectedNote>();

                foreach (var linkedCellEntity in _linkedCellsFilter.Value)
                {
                    var notes = _linkedCellsFilter.Pools.Inc2.Get(linkedCellEntity).Value.Notes;

                    if (!notes.NoteNumberGO[noteNumber].activeSelf) continue;

                    affectedNotes.Add(new AffectedNote
                    {
                        CellEntity = linkedCellEntity,
                        NoteNumber = noteNumber
                    });
                }

                for (int i = 0; i < _config.Value.GridSize; i++)
                {
                    if (_cellsViewPool.Value.Get(cellEntity).Value.Notes.NoteNumberGO[i].activeSelf)
                        affectedNotes.Add(new AffectedNote
                        {
                            CellEntity = cellEntity,
                            NoteNumber = i
                        });
                }

                PlayerMove playerAddMove = new PlayerMove
                {
                    AffectedCell = new AffectedCell {CellEntity = cellEntity, Number = prevNumber},
                    AffectedNotes = affectedNotes
                };

                _sceneData.Value.PlayerMoves.Push(playerAddMove);
            }
        }

        private void EraseMove()
        {
            foreach (var eventEntity in _eraseEvent.Value)
            {
                var cellEntity = _eraseEvent.Pools.Inc1.Get(eventEntity).CellEntity;

                int number = _numberFilter.Pools.Inc1.Get(cellEntity).Value;
                List<AffectedNote> affectedNotes = new List<AffectedNote>();

                if (number == 0)
                {
                    for (int i = 0; i < _config.Value.GridSize; i++)
                    {
                        if (!_cellsViewPool.Value.Get(cellEntity).Value.Notes.NoteNumberGO[i].activeSelf) continue;

                        int noteNumber = i + 1;

                        affectedNotes.Add(new AffectedNote {NoteNumber = noteNumber, CellEntity = cellEntity});
                    }
                }

                if (number == 0 && affectedNotes.Count == 0) continue;

                PlayerMove playerEraseMove = new PlayerMove
                {
                    AffectedCell = new AffectedCell {CellEntity = cellEntity, Number = number},
                    AffectedNotes = affectedNotes
                };

                _sceneData.Value.PlayerMoves.Push(playerEraseMove);
            }
        }
    }
}