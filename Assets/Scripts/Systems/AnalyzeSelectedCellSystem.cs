using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsSudoku.Systems
{
    public class AnalyzeSelectedCellSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Cell, Clicked>> _filter;

        private EcsPoolInject<Clicked> _clickedPool;
        private EcsPoolInject<Selected> _selectedPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var cell = _filter.Pools.Inc1.Get(entity);
                Debug.Log(cell.Value);
            }
        }
    }
}