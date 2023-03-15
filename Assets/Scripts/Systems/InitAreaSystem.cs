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
        public void Init(IEcsSystems systems)
        {
            for (int y = 0; y < _config.Value.GridHeight; y+=3)
            {
                for (int x = 0; x < _config.Value.GridWidth; x+=3)
                {
                    var positionX = x * (1 + _config.Value.Offset.x);
                    var positionY = y * (1 + _config.Value.Offset.y);
                    Object.Instantiate(_config.Value.CellAreaPrefab, new Vector3(positionX, positionY),
                        quaternion.identity);
                }
                
            }
            
        }
    }
}