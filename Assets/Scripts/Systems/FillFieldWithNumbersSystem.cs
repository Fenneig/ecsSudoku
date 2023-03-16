using EcsSudoku.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class FillFieldWithNumbersSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CellViewRef, Number>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var numberToFill = _filter.Pools.Inc2.Get(entity).Value;
                _filter.Pools.Inc1.Get(entity).Value.Number.text = numberToFill == 0 ? "" : numberToFill.ToString();
            }
        }
    }
}