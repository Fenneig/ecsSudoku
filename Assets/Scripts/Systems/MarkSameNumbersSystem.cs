using EcsSudoku.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class MarkSameNumbersSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Clicked, Number>> _clickedFilter = default;
        
        private readonly EcsFilterInject<Inc<Number, SolvedCell>> _toSelectFilter = default;

        private readonly EcsPoolInject<SameNumberAsSelected> _sameNumberAsSelectedPool = default;


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _toSelectFilter.Value)
                _sameNumberAsSelectedPool.Value.Del(entity);

            int number = 0;

            foreach (var entity in _clickedFilter.Value)
                number = _clickedFilter.Pools.Inc2.Get(entity).Value;

            foreach (var entity in _toSelectFilter.Value)
            {
                if (_toSelectFilter.Pools.Inc1.Get(entity).Value == number && 
                    !_sameNumberAsSelectedPool.Value.Has(entity))
                {
                    
                    _sameNumberAsSelectedPool.Value.Add(entity);
                }
            }
        }
    }
}