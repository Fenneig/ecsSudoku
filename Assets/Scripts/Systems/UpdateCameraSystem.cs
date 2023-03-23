using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsSudoku.Systems
{
    public class UpdateCameraSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<SceneData> _sceneData = default;
        private readonly EcsCustomInject<Configuration> _config = default;

        public void Init(IEcsSystems systems)
        {
            var gridSize = _config.Value.GridSize;
            var camera = _sceneData.Value.Camera;
            camera.orthographic = true;

            camera.orthographicSize = _config.Value.GridSize * (1 + _config.Value.Offset) *
                Screen.height / Screen.width / 2f;

            var orthographicSize = (gridSize + (gridSize - 1) * _config.Value.Offset) / 2;
            _sceneData.Value.CameraTransform.position =
                new Vector3(orthographicSize, orthographicSize);
        }
    }
}