using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsSudoku.Systems
{
    public class InitCellViewSystem : IEcsInitSystem
    {
        private EcsFilterInject<Inc<Cell, Position>, Exc<CellViewRef>> _filter = default;
        private EcsCustomInject<Configuration> _config = default;
        private EcsCustomInject<SceneData> _sceneData = default;
        private EcsPoolInject<CellViewRef> _cellViewRefPool = default;

        public void Init(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var position = ref _filter.Pools.Inc2.Get(entity);

                var cellView = Object.Instantiate(_config.Value.CellViewPrefab, _sceneData.Value.FieldTransform);

                cellView.transform.position = new Vector3(position.Value.X + _config.Value.Offset.x * position.Value.X,
                                                          position.Value.Y + _config.Value.Offset.y * position.Value.Y);
                
                cellView.Entity = entity;
                _cellViewRefPool.Value.Add(entity).Value = cellView;
            }
        }
    }
}