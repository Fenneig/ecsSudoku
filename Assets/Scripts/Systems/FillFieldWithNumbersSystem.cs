using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class FillFieldWithNumbersSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<CellViewRef, Number>> _filter = default;
        private readonly EcsFilterInject<Inc<CellClickedEvent>> _event = Idents.Worlds.Events;

        public void Run(IEcsSystems systems)
        {
            foreach (var _ in _event.Value)
            {
                foreach (var entity in _filter.Value)
                {
                    if (_filter.Pools.Inc2.Get(entity).Value == 0) continue;
                    
                    var numberToFill = _filter.Pools.Inc2.Get(entity).Value;
                    _filter.Pools.Inc1.Get(entity).Value.NumberText.text = numberToFill == 0 ? "" : "" + numberToFill;
                }
            }
        }

        public void Init(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var numberToFill = _filter.Pools.Inc2.Get(entity).Value;
                _filter.Pools.Inc1.Get(entity).Value.NumberText.text = numberToFill == 0 ? "" : "" + numberToFill;
            }
        }
    }
}