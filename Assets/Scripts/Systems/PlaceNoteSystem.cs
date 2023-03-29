using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class PlaceNoteSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<SceneData> _sceneData = default;
        
        private readonly EcsFilterInject<Inc<CellClickedEvent>> _cellClickedEventFilter = Idents.Worlds.Events;
        
        private readonly EcsPoolInject<CellViewRef> _cellViews = default;


        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _cellClickedEventFilter.Value)
            {
                var cellEntity = _cellClickedEventFilter.Pools.Inc1.Get(eventEntity).CellEntity;

                if (!_sceneData.Value.NoteMode)
                {
                    _cellViews.Value.Get(cellEntity).Value.Notes.NoteGO.SetActive(false);
                    return;
                }

                var noteNumberToSwitch = _cellClickedEventFilter.Pools.Inc1.Get(eventEntity).Number - 1;
                
                var cellView = _cellViews.Value.Get(cellEntity);

                cellView.Value.Notes.NoteNumber[noteNumberToSwitch]
                    .SetActive(!cellView.Value.Notes.NoteNumber[noteNumberToSwitch].activeSelf);
            }
        }
    }
}