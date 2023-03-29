﻿using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class FillFieldWithNumbersSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<CellViewRef, Number>> _filter = default;
        private readonly EcsFilterInject<Inc<CellClickedEvent>> _cellClickedEvent = Idents.Worlds.Events;
        private readonly EcsFilterInject<Inc<CellEraseEvent>> _cellEraseEvent = Idents.Worlds.Events;

        public void Init(IEcsSystems systems)
        {
            FillField();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var _ in _cellClickedEvent.Value)
            {
                FillField();
            }
            
            foreach (var _ in _cellEraseEvent.Value)
            {
                FillField();
            }
        }

        private void FillField()
        {
            foreach (var entity in _filter.Value)
            {
                var numberToFill = _filter.Pools.Inc2.Get(entity).Value;
                _filter.Pools.Inc1.Get(entity).Value.NumberText.text = numberToFill == 0 ? "" : "" + numberToFill;
            }
        }
    }
}