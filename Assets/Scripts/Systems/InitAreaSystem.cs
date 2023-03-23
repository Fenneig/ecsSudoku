using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;
using UnityEngine;

namespace EcsSudoku.Systems
{
    public class InitAreaSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<Configuration> _config = default;
        private readonly EcsCustomInject<SceneData> _sceneData = default;
        public void Init(IEcsSystems systems)
        {
            for (int x = 0; x < _config.Value.GridSize; x+=3)
            {
                for (int y = 0; y < _config.Value.GridSize; y+=3)
                {
                    var positionX = x * (1 + _config.Value.Offset);
                    var positionY = y * (1 + _config.Value.Offset);
                    Object.Instantiate(_config.Value.CellAreaPrefab, new Vector3(positionX, positionY),
                        quaternion.identity, _sceneData.Value.FieldTransform);
                }
            }
        }
    }
}