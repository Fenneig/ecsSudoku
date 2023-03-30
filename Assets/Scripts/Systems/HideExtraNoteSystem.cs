using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class HideExtraNoteSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CellAddNumberEvent>> _event = Idents.Worlds.Events;
        private readonly EcsFilterInject<Inc<LinkedCell, CellViewRef>> _filter = default;

        private readonly EcsCustomInject<SceneData> _sceneData = default;

        public void Run(IEcsSystems systems)
        {
            if (_sceneData.Value.NoteMode) return;
            
            foreach (var eventEntity in _event.Value)
            {
                var noteNumberToDeactivate = _event.Pools.Inc1.Get(eventEntity).Number - 1;
                
                foreach (var entity in _filter.Value)
                {
                    _filter.Pools.Inc2.Get(entity).Value.Notes.NoteNumber[noteNumberToDeactivate].SetActive(false);
                }
            }
        }
    }
}