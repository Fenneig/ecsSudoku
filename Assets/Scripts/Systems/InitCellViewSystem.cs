﻿using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsSudoku.Systems
{
    public class InitCellViewSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<Cell, Position>, Exc<CellViewRef>> _filter = default;
        private readonly EcsPoolInject<CellViewRef> _cellViewRefPool = default;
        
        private readonly EcsCustomInject<Configuration> _config = default;
        private readonly EcsCustomInject<SceneData> _sceneData = default;

        public void Init(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var position = ref _filter.Pools.Inc2.Get(entity);

                var cellView = Object.Instantiate(_config.Value.CellViewPrefab, _sceneData.Value.FieldTransform);

                cellView.transform.position = new Vector3(position.Value.X + _config.Value.Offset * position.Value.X,
                                                          position.Value.Y + _config.Value.Offset * position.Value.Y);
                
                cellView.Entity = entity;
                _cellViewRefPool.Value.Add(entity).Value = cellView;
            }
        }
    }
}