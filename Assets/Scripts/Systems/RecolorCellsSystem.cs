﻿using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class RecolorCellsSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CellViewRef>> _cellViewsFilter = default;
        
        private readonly EcsPoolInject<Clicked> _clickedPool = default;
        private readonly EcsPoolInject<LinkedCell> _linkedCellsPool = default;
        private readonly EcsPoolInject<SameNumberAsSelected> _sameNumberAsSelectedPool = default;
        private readonly EcsPoolInject<MistakeCell> _mistakeCellsPool = default;
        private readonly EcsPoolInject<PlacedNumber> _placedCellsPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _cellViewsFilter.Value)
            {
                ref var cellView = ref _cellViewsFilter.Pools.Inc1.Get(entity).Value;
                cellView.Background.color = Idents.Colors.UnselectedCell;

                if (_linkedCellsPool.Value.Has(entity))
                    cellView.Background.color = Idents.Colors.LinkedCell;

                if (_sameNumberAsSelectedPool.Value.Has(entity))
                    cellView.Background.color = Idents.Colors.LinkedCell;

                if (_clickedPool.Value.Has(entity))
                    cellView.Background.color = Idents.Colors.SelectedCell;

                if (_mistakeCellsPool.Value.Has(entity) && !_clickedPool.Value.Has(entity))
                    cellView.Background.color = Idents.Colors.MistakeCell;

                if (_placedCellsPool.Value.Has(entity))
                    cellView.NumberText.color = Idents.Colors.PlacedNumber;

                if (_placedCellsPool.Value.Has(entity) && _mistakeCellsPool.Value.Has(entity))
                    cellView.NumberText.color = Idents.Colors.MistakeNumber;
            }
        }
    }
}