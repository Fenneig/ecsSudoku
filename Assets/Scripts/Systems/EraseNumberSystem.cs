using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class EraseNumberSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CellEraseEvent>> _eraseEvent = Idents.Worlds.Events;
        private readonly EcsFilterInject<Inc<Number, CellViewRef, PlacedNumber>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _eraseEvent.Value)
            {
                var cellEntity = _eraseEvent.Pools.Inc1.Get(entity).CellEntity;
                
                _filter.Pools.Inc1.Get(cellEntity).Value = 0;
                
                foreach (var noteNumber in _filter.Pools.Inc2.Get(cellEntity).Value.Notes.NoteNumberGO)
                    noteNumber.SetActive(false);
                
                _filter.Pools.Inc2.Get(cellEntity).Value.Notes.NoteGO.SetActive(true);

                _filter.Pools.Inc3.Del(cellEntity);
            }
        }
    }
}